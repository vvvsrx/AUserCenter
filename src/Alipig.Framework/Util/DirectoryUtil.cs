using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Alipig.Framework.Util
{
    public class DirectoryUtil
    {
        /// <summary>    
        /// 确保文件夹被创建    
        /// </summary>    
        /// <param name="filePath">文件夹全名（含路径）</param>
        public static void AssertDirExist(string filePath)
        {
            DirectoryInfo info = new DirectoryInfo(filePath);
            if (!info.Exists)
            {
                info.Create();
            }
        }

        /// <summary>    
        /// 检测指定目录中是否存在指定的文件,若要搜索子目录请使用重载方法.    
        /// </summary>    
        /// <param name="directoryPath">指定目录的绝对路径</param>    
        /// <param name="searchPattern">模式字符串，"*"代表0或N个字符，"?"代表1个字符。    
        /// 范例："Log*.xml"表示搜索所有以Log开头的Xml文件。</param>  
        public static bool ContainFile(string directoryPath, string searchPattern)
        {
            bool flag;
            try
            {
                if (GetFileNames(directoryPath, searchPattern, false).Length == 0)
                {
                    return false;
                }
                flag = true;
            }
            catch (IOException exception)
            {
                throw exception;
            }
            return flag;
        }

        /// <summary>    
        /// 检测指定目录中是否存在指定的文件,若要搜索子目录请使用重载方法.    
        /// </summary>    
        /// <param name="directoryPath">指定目录的绝对路径</param>    
        /// <param name="searchPattern">模式字符串，"*"代表0或N个字符，"?"代表1个字符。    
        /// 范例："Log*.xml"表示搜索所有以Log开头的Xml文件。</param>    
        public static bool ContainFile(string directoryPath, string searchPattern, bool isSearchChild)
        {
            bool flag;
            try
            {
                if (GetFileNames(directoryPath, searchPattern, true).Length == 0)
                {
                    return false;
                }
                flag = true;
            }
            catch (IOException exception)
            {
                throw exception;
            }
            return flag;
        }


        public static ulong ConvertByteCountToKByteCount(ulong byteCount)
        {
            return (byteCount / ((ulong)0x400L));
        }

        public static float ConvertKByteCountToMByteCount(ulong kByteCount)
        {
            return (float)(kByteCount / ((ulong)0x400L));
        }

        public static float ConvertMByteCountToGByteCount(float kByteCount)
        {
            return (kByteCount / 1024f);
        }

        /// <summary>    
        /// 创建一个目录    
        /// </summary>    
        /// <param name="directoryPath">目录的绝对路径</param>   
        public static void CreateDirectory(string directoryPath)
        {
            if (!Directory.Exists(directoryPath))
            {
                Directory.CreateDirectory(directoryPath);
            }
        }

        /// <summary>    
        /// 生成日期 文件夹    格式：yyyy\mm\dd    
        /// </summary>    
        /// <remarks>    
        /// 生成时间目录   返回 例如： c:\directory\2009\03\01    
        /// </remarks>    
        /// <param name="rootPath">绝对路径   [在此目录下建日期目录]</param>    
        /// <returns>返回完整路径  </returns>  
        public static string CreateDirectoryByDate(string rootPath)
        {
            return CreateDirectoryByDate(rootPath, "yyyy-MM-dd");
        }

        /// <summary>    
        /// 相应格式生成日期目录    
        /// </summary>    
        /// <remarks>    
        /// formatString:    
        ///              yyyy-MM-dd        :2009\03\01    
        ///              yyyy-MM-dd-HH     :2009\03\01\01    
        /// </remarks>    
        /// <param name="rootPath">绝对路径   [在此目录下建日期目录]</param>    
        /// <param name="formatString">格式</param>    
        /// <returns>返回完整路径 </returns>   
        public static string CreateDirectoryByDate(string rootPath, string formatString)
        {
            string str2;
            if (!IsExistDirectory(rootPath))
            {
                throw new DirectoryNotFoundException("the rootPath is not found");
            }
            bool flag = false;
            string str = formatString;
            if (str == null)
            {
                goto Label_003C;
            }
            if (!(str == "yyyy-MM-dd"))
            {
                if (!(str == "yyyy-MM-dd-HH"))
                {
                    goto Label_003C;
                }
                flag = true;
            }
            else
            {
                flag = false;
            }
            goto Label_003E;
        Label_003C:
            flag = false;
        Label_003E:
            str2 = rootPath + @"\" + DateTime.Now.Year.ToString();
            CreateDirectory(str2);
            str2 = str2 + @"\" + DateTime.Now.Month.ToString("00");
            CreateDirectory(str2);
            str2 = str2 + @"\" + DateTime.Now.Day.ToString("00");
            CreateDirectory(str2);
            if (flag)
            {
                str2 = str2 + @"\" + DateTime.Now.Hour.ToString("00");
                CreateDirectory(str2);
            }
            return str2;
        }

        /// <summary>    
        /// 删除指定目录及其所有子目录    
        /// </summary>    
        /// <param name="directoryPath">指定目录的绝对路径</param>   
        public static void DeleteDirectory(string directoryPath)
        {
            if (Directory.Exists(directoryPath))
            {
                Directory.Delete(directoryPath, true);
            }
        }

        /// <summary>    
        /// 取系统所有的逻辑驱动器    
        /// </summary>    
        /// <returns></returns>   
        public static DriveInfo[] GetAllDrives()
        {
            return DriveInfo.GetDrives();
        }


        public static string GetCurrentDirectory()
        {
            return Directory.GetCurrentDirectory();
        }

        /// <summary>    
        /// 获取指定目录中所有子目录列表,若要搜索嵌套的子目录列表,请使用重载方法.    
        /// </summary>    
        /// <param name="directoryPath">指定目录的绝对路径</param> 
        public static string[] GetDirectories(string directoryPath)
        {
            string[] directories;
            try
            {
                directories = Directory.GetDirectories(directoryPath);
            }
            catch (IOException exception)
            {
                throw exception;
            }
            return directories;
        }

        /// <summary>    
        /// 获取指定目录及子目录中所有子目录列表    
        /// </summary>    
        /// <param name="directoryPath">指定目录的绝对路径</param>    
        /// <param name="searchPattern">模式字符串，"*"代表0或N个字符，"?"代表1个字符。    
        /// 范例："Log*.xml"表示搜索所有以Log开头的Xml文件。</param>    
        /// <param name="isSearchChild">是否搜索子目录</param>  
        public static string[] GetDirectories(string directoryPath, string searchPattern, bool isSearchChild)
        {
            string[] strArray;
            try
            {
                if (isSearchChild)
                {
                    return Directory.GetDirectories(directoryPath, searchPattern, SearchOption.AllDirectories);
                }
                strArray = Directory.GetDirectories(directoryPath, searchPattern, SearchOption.TopDirectoryOnly);
            }
            catch (IOException exception)
            {
                throw exception;
            }
            return strArray;
        }

        /// <summary>    
        /// 获取指定目录中所有文件列表    
        /// </summary>    
        /// <param name="directoryPath">指定目录的绝对路径</param> 
        public static string[] GetFileNames(string directoryPath)
        {
            if (!IsExistDirectory(directoryPath))
            {
                throw new FileNotFoundException();
            }
            return Directory.GetFiles(directoryPath);
        }

        /// <summary>    
        /// 获取指定目录及子目录中所有文件列表    
        /// </summary>    
        /// <param name="directoryPath">指定目录的绝对路径</param>    
        /// <param name="searchPattern">模式字符串，"*"代表0或N个字符，"?"代表1个字符。    
        /// 范例："Log*.xml"表示搜索所有以Log开头的Xml文件。</param>    
        /// <param name="isSearchChild">是否搜索子目录</param> 
        public static string[] GetFileNames(string directoryPath, string searchPattern, bool isSearchChild)
        {
            string[] strArray;
            if (!IsExistDirectory(directoryPath))
            {
                throw new FileNotFoundException();
            }
            try
            {
                if (isSearchChild)
                {
                    return Directory.GetFiles(directoryPath, searchPattern, SearchOption.AllDirectories);
                }
                strArray = Directory.GetFiles(directoryPath, searchPattern, SearchOption.TopDirectoryOnly);
            }
            catch (IOException exception)
            {
                throw exception;
            }
            return strArray;
        }

        /// <summary>    
        /// 获取驱动盘符的可用空间大小    
        /// </summary>    
        /// <param name="driveName">Direve name</param>    
        /// <returns>free space (byte)</returns>  
        public static ulong GetFreeSpace(string driveName)
        {
            ulong availableFreeSpace = 0L;
            try
            {
                DriveInfo info = new DriveInfo(driveName);
                availableFreeSpace = (ulong)info.AvailableFreeSpace;
            }
            catch
            {
            }
            return availableFreeSpace;
        }


        public static char[] GetInvalidPathChars()
        {
            return Path.GetInvalidPathChars();
        }

        /// <summary>    
        /// 取系统的特别目录    
        /// </summary>    
        /// <param name="folderType"></param>    
        /// <returns></returns>
        public static string GetSpeicalFolder(Environment.SpecialFolder folderType)
        {
            return Environment.GetFolderPath(folderType);
        }

        /// <summary>    
        /// 取系统目录    
        /// </summary>    
        /// <returns></returns>
        public static string GetSystemDirectory()
        {
            return Environment.SystemDirectory;
        }

        /// <summary>    
        /// 返回当前系统的临时目录    
        /// </summary>    
        /// <returns></returns>
        public static string GetTempPath()
        {
            return Path.GetTempPath();
        }

        /// <summary>    
        /// 检查磁盘是否有足够的可用空间    
        /// </summary>    
        /// <param name="path"></param>    
        /// <param name="requiredSpace"></param>    
        /// <returns></returns>    
        public static bool IsDiskSpaceEnough(string path, ulong requiredSpace)
        {
            ulong freeSpace = GetFreeSpace(Path.GetPathRoot(path));
            return (requiredSpace <= freeSpace);
        }

        /// <summary>    
        /// 检测指定目录是否为空    
        /// </summary>    
        /// <param name="directoryPath">指定目录的绝对路径</param> 
        public static bool IsEmptyDirectory(string directoryPath)
        {
            bool flag;
            try
            {
                if (GetFileNames(directoryPath).Length > 0)
                {
                    return false;
                }
                if (GetDirectories(directoryPath).Length > 0)
                {
                    return false;
                }
                flag = true;
            }
            catch (IOException exception)
            {
                throw exception;
            }
            return flag;
        }

        /// <summary>    
        /// 检测指定目录是否存在    
        /// </summary>    
        /// <param name="directoryPath">目录的绝对路径</param> 
        public static bool IsExistDirectory(string directoryPath)
        {
            return Directory.Exists(directoryPath);
        }

        /// <summary>    
        ///检查目录是否可写，如果可以，返回True，否则False    
        /// </summary>    
        /// <param name="path"></param>    
        /// <returns></returns>   
        public static bool IsWriteable(string path)
        {
            if (!Directory.Exists(path))
            {
                try
                {
                    Directory.CreateDirectory(path);
                }
                catch
                {
                    return false;
                }
            }
            try
            {
                string str = ".test." + Guid.NewGuid().ToString().Substring(0, 5);
                string str2 = Path.Combine(path, str);
                File.WriteAllLines(str2, new string[] { "test" });
                File.Delete(str2);
            }
            catch
            {
                return false;
            }
            return true;
        }


        public static void SetCurrentDirectory(string path)
        {
            Directory.SetCurrentDirectory(path);
        }
    }
}
