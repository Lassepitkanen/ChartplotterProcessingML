using ChartplotterDataProcessorML.FileIO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChartplotterDataProcessorML.UI
{
    public class FolderChooser
    {
        private readonly IFileRepository _fileRepository;
        private readonly FolderInfo _folderInfo;
        private readonly InputValidator _inputValidator;

        public FolderChooser(IFileRepository fileRepository, FolderInfo folderInfo, InputValidator inputValidator)
        {
            _fileRepository = fileRepository;
            _folderInfo = folderInfo;
            _inputValidator = inputValidator;
        }

        public string ChooseFolder()
        {
            Console.Clear();
            var folders = _fileRepository.FindFolders(_fileRepository.InputCsvDir);
            _folderInfo.ShowFolderInfo(folders);

            Console.Write("Folder ID:");
            var id = _inputValidator.Validate(folders.Length);
            return folders[id - 1];
        }
    }
}
