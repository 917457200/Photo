using System;
using System.Runtime.InteropServices;

namespace DeviceScan
{
	/// <summary>
	/// common 的摘要说明。
	/// </summary>
	public class CardScanCommon
	{
		public CardScanCommon()
		{
			//
			// TODO: 在此处添加构造函数逻辑
			//
		}
			public int icdev ; // 通讯设备标识符
			[DllImport("mwhrf_bj.dll", EntryPoint="Open_USB",  SetLastError=true,
			 CharSet=CharSet.Auto , ExactSpelling=false,
			 CallingConvention=CallingConvention.StdCall)]
		    //说明：初始化通讯接口
		    public static extern int Open_USB();

		    [DllImport("mwhrf_bj.dll", EntryPoint="Close_USB",  SetLastError=true,
			 CharSet=CharSet.Auto, ExactSpelling=false,
			 CallingConvention=CallingConvention.StdCall)]
             //说明：    关闭通讯口
        public static extern Int16 Close_USB(int icdev);

		[DllImport("mwhrf_bj.dll", EntryPoint="rf_get_status",  SetLastError=true,
			 CharSet=CharSet.Auto, ExactSpelling=false,
			 CallingConvention=CallingConvention.StdCall)]
        //说明：     返回设备当前状态
		public static extern Int16 rf_get_status(int icdev,[MarshalAs(UnmanagedType.LPArray)]byte[] state);

		[DllImport("mwhrf_bj.dll", EntryPoint="rf_beep",  SetLastError=true,
			 CharSet=CharSet.Auto, ExactSpelling=false,
			 CallingConvention=CallingConvention.StdCall)]
			//说明：     返回设备当前状态
		public static extern Int16 rf_beep(int icdev,int msec);

		[DllImport("mwhrf_bj.dll", EntryPoint="rf_load_key",  SetLastError=true,
			 CharSet=CharSet.Auto, ExactSpelling=false,
			 CallingConvention=CallingConvention.StdCall)]
			//说明：     返回设备当前状态
		public static extern Int16 rf_load_key(int icdev,int mode, int secnr,[MarshalAs(UnmanagedType.LPArray)]byte[] keybuff );

		[DllImport("mwhrf_bj.dll", EntryPoint="rf_load_key_hex",  SetLastError=true,
			 CharSet=CharSet.Auto, ExactSpelling=false,
			 CallingConvention=CallingConvention.StdCall)]
			//说明：     返回设备当前状态
		public static extern Int16 rf_load_key_hex(int icdev,int mode, int secnr,[MarshalAs(UnmanagedType.LPArray)]byte[] keybuff );


		[DllImport("mwhrf_bj.dll", EntryPoint="a_hex",  SetLastError=true,
			 CharSet=CharSet.Auto, ExactSpelling=false,
			 CallingConvention=CallingConvention.StdCall)]
			//说明：     返回设备当前状态
		public static extern Int16 a_hex([MarshalAs(UnmanagedType.LPArray)]byte[] asc,[MarshalAs(UnmanagedType.LPArray)]byte[] hex, int len );

		[DllImport("mwhrf_bj.dll", EntryPoint="hex_a",  SetLastError=true,
			 CharSet=CharSet.Auto, ExactSpelling=false,
			 CallingConvention=CallingConvention.StdCall)]
			//说明：     返回设备当前状态
		public static extern Int16 hex_a([MarshalAs(UnmanagedType.LPArray)]byte[] hex,[MarshalAs(UnmanagedType.LPArray)]byte[] asc, int len );
		
		[DllImport("mwhrf_bj.dll", EntryPoint="rf_reset",  SetLastError=true,
			 CharSet=CharSet.Auto, ExactSpelling=false,
			 CallingConvention=CallingConvention.StdCall)]
			//说明：     返回设备当前状态
		public static extern Int16 rf_reset(int icdev, int msec);
		
		[DllImport("mwhrf_bj.dll", EntryPoint="rf_clr_control_bit",  SetLastError=true,
			 CharSet=CharSet.Auto, ExactSpelling=false,
			 CallingConvention=CallingConvention.StdCall)]
			//说明：     返回设备当前状态
		public static extern Int16 rf_clr_control_bit(int icdev,int _b);

		
		[DllImport("mwhrf_bj.dll", EntryPoint="rf_set_control_bit",  SetLastError=true,
			 CharSet=CharSet.Auto, ExactSpelling=false,
			 CallingConvention=CallingConvention.StdCall)]
			//说明：     返回设备当前状态
		public static extern Int16 rf_set_control_bit(int icdev, int _b);
		
		[DllImport("mwhrf_bj.dll", EntryPoint="rf_disp8",  SetLastError=true,
			 CharSet=CharSet.Auto, ExactSpelling=false,
			 CallingConvention=CallingConvention.StdCall)]
			//说明：     返回设备当前状态
		public static extern Int16 rf_disp8(int icdev, short mode, [MarshalAs(UnmanagedType.LPArray)]byte[] disp);
		
		[DllImport("mwhrf_bj.dll", EntryPoint="rf_disp",  SetLastError=true,
			 CharSet=CharSet.Auto, ExactSpelling=false,
			 CallingConvention=CallingConvention.StdCall)]
			//说明：     返回设备当前状态
		public static extern Int16 rf_disp(int icdev,short mode, int digit);
		
		[DllImport("mwhrf_bj.dll", EntryPoint="rf_encrypt",  SetLastError=true,
			 CharSet=CharSet.Auto, ExactSpelling=false,
			 CallingConvention=CallingConvention.StdCall)]
			//说明：     返回设备当前状态
		public static extern Int16 rf_encrypt([MarshalAs(UnmanagedType.LPArray)]byte[] key,[MarshalAs(UnmanagedType.LPArray)]byte[] ptrsource, int len,[MarshalAs(UnmanagedType.LPArray)]byte[] ptrdest);
		
		[DllImport("mwhrf_bj.dll", EntryPoint="rf_decrypt",  SetLastError=true,
			 CharSet=CharSet.Auto, ExactSpelling=false,
			 CallingConvention=CallingConvention.StdCall)]
			//说明：     返回设备当前状态
		public static extern Int16 rf_decrypt([MarshalAs(UnmanagedType.LPArray)]byte[] key,[MarshalAs(UnmanagedType.LPArray)]byte[] ptrsource, int len,[MarshalAs(UnmanagedType.LPArray)]byte[] ptrdest);
		
		[DllImport("mwhrf_bj.dll", EntryPoint="rf_srd_eeprom",  SetLastError=true,
			 CharSet=CharSet.Auto, ExactSpelling=false,
			 CallingConvention=CallingConvention.StdCall)]
			//说明：     返回设备当前状态
		public static extern Int16 rf_srd_eeprom(int icdev, int offset, int len, [MarshalAs(UnmanagedType.LPArray)]byte[] databuff);
		
		[DllImport("mwhrf_bj.dll", EntryPoint="rf_swr_eeprom",  SetLastError=true,
			 CharSet=CharSet.Auto, ExactSpelling=false,
			 CallingConvention=CallingConvention.StdCall)]
			//说明：     返回设备当前状态
		public static extern Int16 rf_swr_eeprom(int icdev, int offset, int len, [MarshalAs(UnmanagedType.LPArray)]byte[] databuff);
		
		[DllImport("mwhrf_bj.dll", EntryPoint="rf_setport",  SetLastError=true,
			 CharSet=CharSet.Auto, ExactSpelling=false,
			 CallingConvention=CallingConvention.StdCall)]
			//说明：     返回设备当前状态
		public static extern Int16 rf_setport(int icdev, byte _byte);
		
		[DllImport("mwhrf_bj.dll", EntryPoint="rf_getport",  SetLastError=true,
			 CharSet=CharSet.Auto, ExactSpelling=false,
			 CallingConvention=CallingConvention.StdCall)]
			//说明：     返回设备当前状态
		public static extern Int16 rf_getport(int icdev,  out byte _byte);
		
		[DllImport("mwhrf_bj.dll", EntryPoint="rf_gettime",  SetLastError=true,
			 CharSet=CharSet.Auto, ExactSpelling=false,
			 CallingConvention=CallingConvention.StdCall)]
			//说明：     返回设备当前状态
		public static extern Int16 rf_gettime(int icdev, [MarshalAs(UnmanagedType.LPArray)]byte[] time);
		
		
		[DllImport("mwhrf_bj.dll", EntryPoint="rf_gettime_hex",  SetLastError=true,
			 CharSet=CharSet.Auto, ExactSpelling=false,
			 CallingConvention=CallingConvention.StdCall)]
			//说明：     返回设备当前状态
		public static extern Int16 rf_gettime_hex(int icdev, [MarshalAs(UnmanagedType.LPArray)]byte[] time);

		[DllImport("mwhrf_bj.dll", EntryPoint="rf_settime_hex",  SetLastError=true,
			 CharSet=CharSet.Auto, ExactSpelling=false,
			 CallingConvention=CallingConvention.StdCall)]
			//说明：     返回设备当前状态
		public static extern Int16 rf_settime_hex(int icdev, [MarshalAs(UnmanagedType.LPArray)]byte[] time);

		[DllImport("mwhrf_bj.dll", EntryPoint="rf_settime",  SetLastError=true,
			 CharSet=CharSet.Auto, ExactSpelling=false,
			 CallingConvention=CallingConvention.StdCall)]
			//说明：     返回设备当前状态
		public static extern Int16 rf_settime(int icdev, [MarshalAs(UnmanagedType.LPArray)]byte[] time);

		[DllImport("mwhrf_bj.dll", EntryPoint=" rf_setbright",  SetLastError=true,
			 CharSet=CharSet.Auto, ExactSpelling=false,
			 CallingConvention=CallingConvention.StdCall)]
			//说明：     返回设备当前状态
		public static extern Int16  rf_setbright(int icdev, byte bright);

		[DllImport("mwhrf_bj.dll", EntryPoint="rf_ctl_mode",  SetLastError=true,
			 CharSet=CharSet.Auto, ExactSpelling=false,
			 CallingConvention=CallingConvention.StdCall)]
			//说明：     返回设备当前状态
		public static extern Int16 rf_ctl_mode(int icdev, int mode);

		[DllImport("mwhrf_bj.dll", EntryPoint="rf_disp_mode",  SetLastError=true,
			 CharSet=CharSet.Auto, ExactSpelling=false,
			 CallingConvention=CallingConvention.StdCall)]
			//说明：     返回设备当前状态
		public static extern Int16 rf_disp_mode(int icdev, int mode);

		[DllImport("mwhrf_bj.dll", EntryPoint="lib_ver",  SetLastError=true,
			 CharSet=CharSet.Auto, ExactSpelling=false,
			 CallingConvention=CallingConvention.StdCall)]
			//说明：     返回设备当前状态
		public static extern Int16 lib_ver([MarshalAs(UnmanagedType.LPArray)]byte[] ver);

		
	}
}
