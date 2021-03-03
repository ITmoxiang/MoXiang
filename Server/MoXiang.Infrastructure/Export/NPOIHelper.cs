using System;
using System.Collections.Generic;
using System.Text;
using NPOI;
using NPOI.XSSF.Model;
using NPOI.HSSF.Model;
using System.Threading.Tasks;
using NPOI.SS.UserModel;
using System.Data;
using System.Reflection;
using NPOI.XSSF.UserModel;
using System.IO;
using System.Linq;
using System.ComponentModel;

namespace MoXiang.Infrastructure.Export
{
    public class NPOIHelper
    {
        private static object obj = new object();

        /// <summary>
        /// 打印不带标题excel
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="data"></param>
        /// <returns></returns>
        public static async Task<byte[]> ExporterExcel<T>(List<T> data) where T : class
        {
            lock (obj)
            {
                DataTable dt = ListToDataTable(data);
                #region NPOI导出
                var fileName = Path.Combine(Directory.GetCurrentDirectory(), "Templates", "ExporterExcel.xlsx");
                //获取文件后缀
                string Suffix = Path.GetExtension(fileName);
                IWorkbook wb = new XSSFWorkbook();
                //创建一个sheet页
                ISheet sheet = wb.CreateSheet("sheet1");
                //创建行标题
                IRow header = sheet.CreateRow(0);
                //遍历插入行标题
                //for (int i = 0; i < dt.Columns.Count; i++)
                //{
                //    header.CreateCell(i).SetCellValue(dt.Columns[i].ToString());
                //}
                //遍历插入内容
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    //由于第一行写标题了所以是从第二行开始
                    header = sheet.CreateRow(i);
                    //写这一行的数据
                    for (int j = 0; j < dt.Columns.Count; j++)
                    {
                        //同理也是从左往右写入数据
                        header.CreateCell(j).SetCellValue(dt.Rows[i][j].ToString());
                        sheet.AutoSizeColumn(j);
                    }
                }
                //读取文件，如找不到此文件，创建新文件
                using (FileStream xlsfile = new FileStream(fileName, FileMode.OpenOrCreate))
                {
                    //写入数据
                    wb.Write(xlsfile);
                }
                using (FileStream fs = new FileStream(fileName, FileMode.Open, FileAccess.Read)) 
                {
                    byte[] buffur = new byte[fs.Length];
                    fs.Read(buffur, 0, (int)fs.Length);
                    fs.Close();
                    File.Delete(fileName);
                    return buffur;
                };
               
                #endregion
            }

        }
        /// <summary>
        /// 泛型列表List转换为DataTable
        /// </summary>
        /// <typeparam name="T">泛型实体</typeparam>
        /// <param name="list">要转换的列表</param>
        /// <returns></returns>
        private static DataTable ListToDataTable<T>(List<T> list)
        {
            PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(typeof(T));
            DataTable dt = new DataTable();
            for (int i = 0; i < properties.Count; i++)
            {
                PropertyDescriptor property = properties[i];
                dt.Columns.Add(property.Name, property.PropertyType);
            }
            object[] values = new object[properties.Count];
            foreach (T item in list)
            {
                for (int i = 0; i < values.Length; i++)
                {
                    values[i] = properties[i].GetValue(item);
                }
                dt.Rows.Add(values);
            }
            return dt;
        }
    }
}
