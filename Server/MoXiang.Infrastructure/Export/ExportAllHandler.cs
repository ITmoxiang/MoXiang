using Magicodes.ExporterAndImporter.Core;
using Magicodes.ExporterAndImporter.Excel;
using Magicodes.ExporterAndImporter.Html;
using Magicodes.ExporterAndImporter.Pdf;
using Magicodes.ExporterAndImporter.Csv;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Magicodes.ExporterAndImporter.Word;
using Magicodes.ExporterAndImporter.Core.Models;
using Microsoft.Extensions.Logging;
using DinkToPdf;

namespace MoXiang.Infrastructure.Export
{
    public class ExportAllHandler
    {
        #region pdf
        /// <summary>
        /// pdf按模板导出
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="filePath"></param>
        /// <param name="tplPath"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public static async Task ExporterpdfBatch<T>(string filePath, List<T> data, string tplPath = null) where T : class
        {
            if (tplPath == null)
            {
                tplPath = Path.Combine(Directory.GetCurrentDirectory(), "Templates", "default.cshtml");
            }
            else
            {
                tplPath = Path.Combine(Directory.GetCurrentDirectory(), "Templates", tplPath);
            }

            var tpl = System.IO.File.ReadAllText(tplPath);
            int num = 0;
            var Model = new List<T>();
            string directory = Path.GetDirectoryName(filePath);
            string extension = Path.GetExtension(filePath);
            string fileName = Path.GetFileNameWithoutExtension(filePath);
            foreach (var item in data)
            {
                Model.Add(item);
                var FilePathName = Path.Combine(directory, fileName + "(" + ++num + ")" + extension);
                var exporter = new PdfExporter();
                await exporter.ExportListByTemplate(FilePathName, Model, tpl);
                Model.Remove(item);
            }
        }

        /// <summary>
        /// 按模板单个导出
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="filePath"></param>
        /// <param name="data"></param>
        /// <param name="tplPath"></param>
        /// <returns></returns>
        public static async Task<byte[]> Exporterpdf<T>(T data, string tplPath = null, Action<PdfExporterAttribute> action = null) where T : class
        {
            if (tplPath == null)
            {
                tplPath = Path.Combine(Directory.GetCurrentDirectory(), "Templates", "default.cshtml");
            }
            else
            {
                tplPath = Path.Combine(Directory.GetCurrentDirectory(), "Templates", tplPath);
            }
            var tpl = System.IO.File.ReadAllText(tplPath);
            var exporter = new PdfExporter();
            PdfExporterAttribute pdf = new PdfExporterAttribute();
            if (action != null)
                action.Invoke(pdf);
            else
            {
                pdf.IsWriteHtml = true;
                pdf.PaperKind = PaperKind.A4;
                pdf.Orientation = Orientation.Portrait;
                pdf.FooterSettings = new FooterSettings() { FontSize = 5, Right = "Page [page] of [toPage]", Line = false, Spacing = 2.812 };
            }
            var result = await exporter.ExportBytesByTemplate(data, pdf, tpl);
            return result;
        }
        #endregion

        #region Excel

        /// <summary>
        /// Excel导出byte数组
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="filePath"></param>
        /// <param name="tplPath"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public static async Task<byte[]> ExporterExcel(List<dynamic> data)
        {
            IExporter exporter = new ExcelExporter();
            return await exporter.ExportAsByteArray(data);
        }
        #endregion

        #region html
        /// <summary>
        /// html按模板导出
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="filePath"></param>
        /// <param name="tplPath"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public static async Task ExporterHtml<T>(string filePath, List<T> data, string tplPath = null) where T : class
        {
            try
            {
                if (tplPath == null) tplPath = Path.Combine(Directory.GetCurrentDirectory(), "ExportTemplate", "default.cshtml");
                var tpl = File.ReadAllText(tplPath);
                var exporter = new HtmlExporter();
                if (File.Exists(filePath)) File.Delete(filePath);
                var result = await exporter.ExportListByTemplate(filePath, data, tpl);
            }
            catch (Exception e)
            {

                throw;
            }

        }
        #endregion

        #region word
        /// <summary>
        /// Word按模板导出
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="filePath"></param>
        /// <param name="tplPath"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public static async Task ExporterWord<T>(string filePath, List<T> data, string tplPath = null) where T : class
        {
            try
            {
                if (tplPath == null) tplPath = Path.Combine(Directory.GetCurrentDirectory(), "ExportTemplate", "default.cshtml");
                var tpl = File.ReadAllText(tplPath);
                var exporter = new WordExporter();
                if (File.Exists(filePath)) File.Delete(filePath);
                var result = await exporter.ExportListByTemplate(filePath, data, tpl);
            }
            catch (Exception)
            {

                throw;
            }

        }
        #endregion

        #region csv
        /// <summary>
        /// Csv导出
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="filePath"></param>
        /// <param name="tplPath"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public static async Task ExporterCsv(string filePath, List<dynamic> data)
        {
            try
            {
                IExporter exporter = new CsvExporter();
                var result = await exporter.ExportAsByteArray(data);
                File.WriteAllBytes(filePath, result);
            }
            catch (Exception)
            {

                throw;
            }

        }
        #endregion
    }
}
