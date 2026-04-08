using MailingApp.Dtos.Generals;

namespace MailingApp.Utilities
{
    public class FileUploadUtil
    {
        private readonly string _temporaryFolder = Path.Combine(Directory.GetCurrentDirectory(), Const.PATH_TEMPORARY_DATA);
        private readonly string[] _allowedExtensionImages = { ".jpg", ".jpeg", ".png" };
        private readonly string[] _allowedExtensionDocPDF = { ".pdf" };
        private readonly string[] _allowedExtensionDocuments = { ".jpg", ".jpeg", ".png", ".pdf", ".xlsx" };
        public async Task<(ResStatusFailedDto resStatus, string? url)> UploadImage(IFormFile file)
        {
            if (file == null || file.Length == 0)
                return (new ResStatusFailedDto(Const.RESP_FAILED_MANDATORY, Const.RESP_FAILED_MANDATORY_FILE, Const.HTTP_CODE_BAD_REQUEST), null);

            // Validasi ekstensi file
            string extension = Path.GetExtension(file.FileName).ToLowerInvariant();
            if (!_allowedExtensionImages.Contains(extension))
                return (new ResStatusFailedDto(Const.RESP_FAILED_MANDATORY, Const.RESP_REQPARAM_FORMAT_FILE_IMAGE, Const.HTTP_CODE_BAD_REQUEST), null);

            if (!Directory.Exists(_temporaryFolder))
                Directory.CreateDirectory(_temporaryFolder);

            string fileName = GetUniqueFileName(_temporaryFolder, file.FileName);
            string filePath = Path.Combine(_temporaryFolder, fileName);

            using (var stream = new FileStream(filePath, FileMode.CreateNew))
            {
                await file.CopyToAsync(stream);
            }

            string fileUrl = $"{Environment.GetEnvironmentVariable("BASE_URL")}/{Const.RELATIVE_PATH_TEMPORARY_DATA}/{fileName}";
            return (new ResStatusFailedDto(Const.RES_SUCCESS, Const.RES_SUCCESS, Const.HTTP_CODE_SUCCESS), fileUrl);
        }

        public async Task<(ResStatusFailedDto resStatus, string? url)> UploadDocPDF(IFormFile file)
        {
            if (file == null || file.Length == 0)
                return (new ResStatusFailedDto(Const.RESP_FAILED_MANDATORY, Const.RESP_FAILED_MANDATORY_FILE, Const.HTTP_CODE_BAD_REQUEST), null);

            // Validasi ekstensi file
            string extension = Path.GetExtension(file.FileName).ToLowerInvariant();
            if (!_allowedExtensionDocPDF.Contains(extension))
                return (new ResStatusFailedDto(Const.RESP_FAILED_MANDATORY, Const.RESP_REQPARAM_FORMAT_FILE_IMAGE, Const.HTTP_CODE_BAD_REQUEST), null);

            if (!Directory.Exists(_temporaryFolder))
                Directory.CreateDirectory(_temporaryFolder);

            string fileName = GetUniqueFileName(_temporaryFolder, file.FileName);
            string filePath = Path.Combine(_temporaryFolder, fileName);

            using (var stream = new FileStream(filePath, FileMode.CreateNew))
            {
                await file.CopyToAsync(stream);
            }

            string fileUrl = $"{Environment.GetEnvironmentVariable("BASE_URL")}/{Const.RELATIVE_PATH_TEMPORARY_DATA}/{fileName}";
            return (new ResStatusFailedDto(Const.RES_SUCCESS, Const.RES_SUCCESS, Const.HTTP_CODE_SUCCESS), fileUrl);
        }

        public async Task<(ResStatusFailedDto resStatus, string? url)> UploadDocuments(IFormFile file)
        {
            if (file == null || file.Length == 0)
                return (new ResStatusFailedDto(Const.RESP_FAILED_MANDATORY, Const.RESP_FAILED_MANDATORY_FILE, Const.HTTP_CODE_BAD_REQUEST), null);

            // Validasi ekstensi file
            string extension = Path.GetExtension(file.FileName).ToLowerInvariant();
            if (!_allowedExtensionDocuments.Contains(extension))
                return (new ResStatusFailedDto(Const.RESP_FAILED_MANDATORY, Const.RESP_REQPARAM_FORMAT_FILE_IMAGE, Const.HTTP_CODE_BAD_REQUEST), null);

            if (!Directory.Exists(_temporaryFolder))
                Directory.CreateDirectory(_temporaryFolder);

            string fileName = GetUniqueFileName(_temporaryFolder, file.FileName);
            string filePath = Path.Combine(_temporaryFolder, fileName);

            using (var stream = new FileStream(filePath, FileMode.CreateNew))
            {
                await file.CopyToAsync(stream);
            }

            string fileUrl = $"{Environment.GetEnvironmentVariable("BASE_URL")}/{Const.RELATIVE_PATH_TEMPORARY_DATA}/{fileName}";
            return (new ResStatusFailedDto(Const.RES_SUCCESS, Const.RES_SUCCESS, Const.HTTP_CODE_SUCCESS), fileUrl);
        }

        public string MoveFile(List<string> urls, string path, string pathRelative)
        {
            List<string> movedFiles = new List<string>();

            foreach (string url in urls)
            {
                string fileName = Path.GetFileName(
                    Uri.UnescapeDataString(new Uri(url).AbsolutePath)
                );

                string sourcePath = Path.Combine(Const.PATH_TEMPORARY_DATA, fileName);

                if (!File.Exists(sourcePath))
                    continue;

                string safeFileName = GetUniqueFileName(path, fileName);

                string destinationPath = Path.Combine(path, safeFileName);

                File.Move(sourcePath, destinationPath);

                string relativePath = $"/{pathRelative}/{safeFileName}";
                movedFiles.Add(relativePath);
            }

            return string.Join(';', movedFiles);
        }

        private string GetUniqueFileName(string folder, string filename)
        {
            string name = Path.GetFileNameWithoutExtension(filename);
            string ext = Path.GetExtension(filename);
            string fullPath = Path.Combine(folder, filename);

            int count = 1;

            while (System.IO.File.Exists(fullPath))
            {
                string tempName = $"{name}({count}){ext}";
                fullPath = Path.Combine(folder, tempName);
                count++;
            }

            return Path.GetFileName(fullPath);
        }
    }
}