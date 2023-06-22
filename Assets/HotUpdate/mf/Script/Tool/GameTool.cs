//using Framework.Lua;
//using Framework.Resource;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Framework.Tool
{
    public partial class GameTool
    {
        /// <summary>
        /// 加载预制并创建
        /// </summary>
        //public static void Load(ResourceEnum resourceEnum, string resName, Type type, LuaTable luaAction)
        //{
        //    ResourcesModule.Instance.Load(resourceEnum, resName, type, luaAction);
        //}
        /// <summary>
        /// 加载预制并创建
        /// </summary>
        public static void LoadOrCreate(string resName, int parentId)
        {
            //ResourcesModule.Instance.LoadOrCreate(resName, parentId);
        }
        /// <summary>
        /// 开启一个协程
        /// </summary>
        /// <param name="co"></param>
        public static Coroutine StartCoroutine(IEnumerator func)
        {
            Coroutine cor = null;
            //GameApp.Instance.StartCoroutine(func, out cor);
            return cor;
        }
        /// <summary>
        /// 停止一个协程
        /// </summary>
        /// <param name="co"></param>
        public static void StopCoroutine(Coroutine co)
        {
            //GameApp.Instance.StopCoroutine(co);

        }

        /// <summary>
        /// Short 转换成byte
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static byte[] short2byte(ushort s)
        {
            byte[] b = new byte[2];
            for (int i = 0; i < 2; i++)
            {
                int offset = 16 - (i + 1) * 8; //因为byte占4个字节，所以要计算偏移量
                b[i] = (byte)((s >> offset) & 0xff); //把16位分为2个8位进行分别存储
            }
            return b;
        }

        /// <summary>
        /// byte转换为short
        /// </summary>
        /// <param name="b"></param>
        /// <returns></returns>
        public static ushort byte2short(byte[] b)
        {
            ushort l = 0;
            for (int i = 0; i < 2; i++)
            {
                l <<= 8; //<<=和我们的 +=是一样的，意思就是 l = l << 8 
                l |= (ushort)(b[i] & 0xff); //和上面也是一样的  l = l | (b[i]&0xff)
            }
            return l;
        }

        /// <summary>
        /// int转换为byte
        /// </summary>
        /// <param name="a"></param>
        /// <returns></returns>
        public static byte[] int2Byte(int a)
        {
            byte[] b = new byte[4];
            b[0] = (byte)(a >> 24);
            b[1] = (byte)(a >> 16);
            b[2] = (byte)(a >> 8);
            b[3] = (byte)(a);

            return b;
        }
        /// <summary>
        /// byte转换为int
        /// </summary>
        /// <param name="b"></param>
        /// <returns></returns>
        public static int byte2Int(byte[] b)
        {
            return ((b[0] & 0xff) << 24) | ((b[1] & 0xff) << 16)
                    | ((b[2] & 0xff) << 8) | (b[3] & 0xff);
        }
        public static PerfabDto LoadObj(object path, Transform parent = null)
        {
            return LoadObj(path, parent, Vector3.zero);
        }

        public static PerfabDto LoadObj(object path, Transform parent, Vector3 pos)
        {
            PerfabDto dto = PerfabTool.Instance.GetObj(path);
            GameObject obj = dto.obj;
            obj.transform.SetParent(parent);
            obj.transform.localPosition = pos;
            return dto;
        }
    }
}
