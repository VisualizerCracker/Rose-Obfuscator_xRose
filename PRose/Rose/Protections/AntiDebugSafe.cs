﻿using System;
using System.Runtime.InteropServices;

namespace Rose.Protections
{
	// Token: 0x02000009 RID: 9
	internal static class AntiDebugSafe
	{
		// Token: 0x06000019 RID: 25
		[DllImport("ntdll.dll", CharSet = CharSet.Auto)]
		public static extern int NtQueryInformationProcess(IntPtr test, int test2, int[] test3, int test4, ref int test5);

		// Token: 0x0600001A RID: 26 RVA: 0x00003488 File Offset: 0x00001688
		private static void xEQAW()
		{
			for (;;)
			{
				IntPtr intPtr;
				int[] array;
				int num;
				IntPtr intPtr2;
				if (AntiDebugSafe.NtQueryInformationProcess(intPtr, 0, array, 24, ref num) != 0)
				{
					intPtr2 = Marshal.ReadIntPtr(intPtr, 0);
					goto IL_26;
				}
				goto IL_A1;
				IL_60:
				if (Environment.OSVersion.Platform != PlatformID.Win32NT)
				{
					if (array[0] != 0)
					{
						goto IL_A1;
					}
					continue;
				}
				IL_26:
				array = new int[6];
				IntPtr ptr;
				IntPtr value = Marshal.ReadIntPtr(ptr, 8);
				num = 0;
				if (array[0] != 1)
				{
					goto IL_60;
				}
				IL_73:
				if (AntiDebugSafe.NtQueryInformationProcess(intPtr, 30, array, 4, ref num) != 0)
				{
					continue;
				}
				if (string.Compare(Environment.GetEnvironmentVariable("COR_ENABLE_PROFILING"), "1", StringComparison.Ordinal) == 0)
				{
					break;
				}
				goto IL_60;
				IL_A1:
				intPtr = Marshal.ReadIntPtr(Marshal.ReadIntPtr((IntPtr)array[1], 12), 12);
				ptr = intPtr2;
				Environment.Exit(0);
				IntPtr ptr2;
				Marshal.WriteInt32(ptr2, 0, (int)value);
				goto IL_73;
			}
			Environment.Exit(0);
		}
	}
}
