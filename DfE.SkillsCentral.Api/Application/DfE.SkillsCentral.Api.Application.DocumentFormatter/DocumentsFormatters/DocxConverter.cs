using System.Text;
using Aspose.Words;
using DfE.NCS.Framework.ResourceManager;

namespace DfE.SkillsCentral.Api.Application.DocumentsFormatters;

public static class DocxConverter
{
    static DocxConverter()
    {
        var licenseKey = ResourceManager.GetResource(ResourceEnum.AsposeWordLicense);
        var license = new License();
        license.SetLicense(new MemoryStream(Encoding.UTF8.GetBytes(licenseKey)));
    }

    public static byte[] ConvertDocx(byte[] docxContent, DocxTargetFormat format)
    {
        var targetFormat = GetTargetFormat(format);


        try
        {
            using var docxStream = new MemoryStream();
            docxStream.Write(docxContent, 0, docxContent.Length);
            var converter = new Document(docxStream);
            using var pdfStream = new MemoryStream();
            converter.Save(pdfStream, targetFormat);
            var pdfContent = pdfStream.ToArray();

            return pdfContent;
        }
        catch (Exception ex)
        {
            //TODO: Handle exception
            throw new Exception(
                $"Error while creating pdf report using aspose.words, targetFormat: '{targetFormat.ToString()}'", ex);
        }
    }

    private static SaveFormat GetTargetFormat(DocxTargetFormat format)
    {
        var targetFormat = format switch
        {
            DocxTargetFormat.Doc => SaveFormat.Doc,
            DocxTargetFormat.JPEG => SaveFormat.Jpeg,
            DocxTargetFormat.MHTML => SaveFormat.Mhtml,
            DocxTargetFormat.PDF => SaveFormat.Pdf,
            DocxTargetFormat.Text => SaveFormat.Text,
            DocxTargetFormat.TIFF => SaveFormat.Tiff,
            _ => throw new NotSupportedException(
                $"The specified document conversion format {format.ToString()} is not supported")
        };

        return targetFormat;
    }
}
