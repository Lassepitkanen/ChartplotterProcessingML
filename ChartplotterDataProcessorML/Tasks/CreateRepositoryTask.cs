using ChartplotterDataProcessorML.FileIO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChartplotterDataProcessorML.Tasks
{
    public class CreateRepositoryTask : ITask
    {
        private RepositoryCreator _repositoryCreator;

        public string Description => TaskName.CreateRepository;

        public CreateRepositoryTask(RepositoryCreator repositoryCreator)
        {
            _repositoryCreator = repositoryCreator;
        }

        public void Run(ProcessingContext context)
        {
            _repositoryCreator.CreateFolders();

            Console.Clear();
            Console.WriteLine("Folders created");
        }
    }
}
