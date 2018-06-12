using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;
using System.IO;
using System.Threading;
using System.Net;
using System.ComponentModel;
using Ytg.Chart.Client.NetWork;


namespace Newinfosoft.Net.Sockets
{
    public class AsyncClient : INotifyPropertyChanged
    {
        #region Events
        /// <summary>
        /// 当接收到数据时
        /// </summary>
        public event EventHandler<AsyncConnectionEventArgs> DataRecieved;
        /// <summary>
        /// 当数据发送完毕时
        /// </summary>
        public event EventHandler<AsyncConnectionEventArgs> DataSend;
        /// <summary>
        /// 当连接服务器成功时
        /// </summary>
        public event EventHandler<AsyncConnectionEventArgs> Connected;
        /// <summary>
        /// 当属性改变时（例如是否连接属性）
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;
        #endregion

        /// <summary>
        /// 用于存放接收时临时数据的内部类
        /// 假设服务器发送来的数据格式为：
        /// |数据长度n|数据本生
        ///    4字节    n字节
        /// </summary>
        protected class StateObject
        {
            public byte[] Buffer;
            /// <summary>
            /// 还有多少字节的数据没有接收
            /// </summary>
            public int RemainSize = 0;

            public MemoryStream Stream = null;

            public StateObject(int bufferSize)
            {
                Buffer = new byte[bufferSize];
            }

            ~StateObject()
            {
                if (Stream != null)
                {
                    Stream.Close();
                    Stream.Dispose();
                }
            }
        }

        /// <summary>
        /// 客户端Socket对象
        /// </summary>
        public Socket Client { get; private set; }

        #region 异步SocketAsyncEventArgs
        public SocketAsyncEventArgs SendEventArgs { get; private set; }
        public SocketAsyncEventArgs ReceiveEventArgs { get; private set; }
        public SocketAsyncEventArgs ConnectEventArgs { get; private set; }
        #endregion

        /// <summary>
        /// 读取或设置接收时的StateObject
        /// </summary>
        protected StateObject State { get; set; }

        /// <summary>
        /// 发送锁，只有当前一个包中的数据全部发送完时，才允许发送下一个包
        /// </summary>
        protected object m_lockobject = new object();
        protected ManualResetEvent SendResetEvent = new ManualResetEvent(false);

        #region IsConnecting
        protected bool m_IsConnecting = false;
        /// <summary>
        /// 读取或设置是否连接到远程服务器，可用于绑定操作
        /// </summary>
        public bool IsConnecting
        {
            get
            {
                return m_IsConnecting;
            }
            set
            {
                if (m_IsConnecting != value)
                {
                    m_IsConnecting = value;
                    OnPropertyChanged("IsConnecting");
                }
            }
        }
        #endregion

        /// <summary>
        /// 通过指定的IPAddress和Port创建AsyncClient，需要调用Connect方法连接
        /// </summary>
        /// <param name="bufferSize">接收缓存大小</param>
        public AsyncClient(int bufferSize)
        {
            State = new StateObject(bufferSize);

            SendEventArgs = new SocketAsyncEventArgs();
            ReceiveEventArgs = new SocketAsyncEventArgs();
            ReceiveEventArgs.Completed += OnReceiveCompleted;
            SendEventArgs.Completed += OnSendCompleted;

            IsConnecting = false;
        }

        /// <summary>
        /// 将已有的Socket包装为AsyncClient对象，
        /// 如果Socket没有连接，则需要调用Connect方法
        /// </summary>
        /// <param name="socket">Socket对象</param>
        /// <param name="bufferSize">接收缓存的大小</param>
        public AsyncClient(Socket socket, int bufferSize)
        {
            State = new StateObject(bufferSize);

            SendEventArgs = new SocketAsyncEventArgs();
            ReceiveEventArgs = new SocketAsyncEventArgs();
            ReceiveEventArgs.Completed += OnReceiveCompleted;
            SendEventArgs.Completed += OnSendCompleted;

            this.Client = socket;
            if (socket != null && socket.Connected)
            {
                ReceiveEventArgs.SetBuffer(State.Buffer, 0, State.Buffer.Length);
                Client.ReceiveAsync(ReceiveEventArgs);
                IsConnecting = true;
            }
            else
            {
                IsConnecting = false;
            }
        }

        /// <summary>
        /// 连接
        /// </summary>
        /// <param name="address">IP地址</param>
        /// <param name="port">端口号</param>
        public void Connect(String address, int port)
        {
            if (Client == null)
            {
                Client = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            }
            if (!Client.Connected)
            {
                ConnectEventArgs = new SocketAsyncEventArgs();
                ConnectEventArgs.Completed += OnConnectComplete;
                EndPoint remoteEndPoint = new IPEndPoint(IPAddress.Parse(address), port);
                ConnectEventArgs.RemoteEndPoint = remoteEndPoint;
                Client.ConnectAsync(ConnectEventArgs);
            }
        }

        /// <summary>
        /// 发送数据
        /// </summary>
        /// <param name="data">需要发送的数据</param>
        public void Send(Byte[] data)
        {
            Send(data, 0, data.Length);
        }

        /// <summary>
        /// 发送数据，按照以下格式发送数据：
        /// |数据长度n|需要发送的数据
        ///    4字节        n字节
        /// </summary>
        /// <param name="data">需要发送的数据</param>
        /// <param name="offset">需要发送数据的偏移量</param>
        /// <param name="size">需要发送数据的长度</param>
        public void Send(Byte[] data, int offset, int size)
        {
            if (!IsConnecting)
            {
                throw new Exception("没有连接，无法发送数据!");
            }

            lock (m_lockobject)
            {
                if (data == null || data.Length == 0)
                    return;
                //计算数据的长度，并转换成字节数组，作为本次发送的头部
                //byte[] length = BitConverter.GetBytes(size);
                //Byte[] buffer = new Byte[size + length.Length];

                //Array.Copy(length, 0, buffer, 0, length.Length);
                //Array.Copy(data, offset, buffer, length.Length, size);
                //设置发送Buffer
                SendEventArgs.SetBuffer(data, 0, data.Length);

                SendResetEvent.Reset();
                Client.SendAsync(SendEventArgs);
                //等待发送成功的信息，只有收到该信息，才退出lock锁，
                //这样，确保只有当前面得数据发送完后，才发送下一段数据
                SendResetEvent.WaitOne();
            }
        }

        protected void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        protected void OnConnectComplete(object sender, SocketAsyncEventArgs e)
        {
            if (e.SocketError == SocketError.Success)
            {
                IsConnecting = true;
                ReceiveEventArgs.SetBuffer(State.Buffer, 0, State.Buffer.Length);
                Client.ReceiveAsync(ReceiveEventArgs);
                if (Connected != null)
                {
                    
                    Connected(this, new AsyncConnectionEventArgs(null, this));
                }
            }
            else
            {
                IsConnecting = false;
            }
        }

        void OnSendCompleted(object sender, SocketAsyncEventArgs e)
        {
            //如果传输的数据量为0，则表示链接已经断开
            if (e.BytesTransferred == 0)
            {
                Client.Close();
            }
            else
            {

                if (DataSend != null)
                {
                    AsyncConnectionEventArgs se = new AsyncConnectionEventArgs(e.Buffer, this);
                    DataSend(this, se);
                }
                //通知数据发送完毕
                SendResetEvent.Set();
            }
        }

        /// <summary>
        /// 接收数据处理函数
        /// 1、将收到的数据包中的前4字节转换成Int32类型，作为本次数据包的长度。
        /// 2、将这个值设置成StateObject的RemainSize。
        /// 3、将数据包中剩下的数据写入StateObject的MemoryStream，并减少相应的RemainSize值。
        /// 4、直到RemainSize=0时，表示这一段数据已经接收完毕，从而重复1，开始下一段数据包的接收
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void OnReceiveCompleted(object sender, SocketAsyncEventArgs e)
        {
            //如果传输的数据量为0，则表示链接已经断开
            if (e.BytesTransferred == 0)
            {
                Client.Close();
            }
            else
            {
                int position = 0;
                var c= System.Text.Encoding.UTF8.GetString(e.Buffer, 0, e.Buffer.Length);
                while (position < e.BytesTransferred)
                {
                    if (State.RemainSize > 0)
                    {
                        int bytesToRead = State.RemainSize > e.BytesTransferred - position ?
                            e.BytesTransferred - position : State.RemainSize;
                        State.RemainSize -= bytesToRead;
                        State.Stream.Write(State.Buffer, position, bytesToRead);
                        position += bytesToRead;

                        if (State.RemainSize == 0)
                        {
                            if (DataRecieved != null)
                            {
                                AsyncConnectionEventArgs ce =
                                    new AsyncConnectionEventArgs(State.Stream.ToArray(), this);
                                DataRecieved(this, ce);
                            }
                            State.Stream.Dispose();
                        }

                    }
                    else
                    {
                        State.RemainSize = BitConverter.ToInt32(State.Buffer, position);
                        State.Stream = new MemoryStream(State.RemainSize);
                        position += 4;
                    }
                }
                //重新设置数据缓存区
                e.SetBuffer(State.Buffer, 0, State.Buffer.Length);
                Client.ReceiveAsync(e);
            }
        }
    }
}