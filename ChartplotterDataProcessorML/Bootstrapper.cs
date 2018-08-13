using System;
using System.Collections.Generic;
using System.Linq;
using ChartplotterDataProcessorML.DataProcessing;
using ChartplotterDataProcessorML.FileIO;
using ChartplotterDataProcessorML.ImageRecognition;
using ChartplotterDataProcessorML.UI;
using ChartplotterDataProcessorML.WaterLevelWebCrawler;
using ChartplotterDataProcessorML.ImageRecognition.MachineLearning;
using Serilog;
using Accord.Imaging.Filters;
using System.Drawing;
using ChartplotterDataProcessorML.ImageRecognition.Models;
using ChartplotterDataProcessorML.Tasks;

namespace ChartplotterDataProcessorML
{
    public class Bootstrapper
    {
        public UIMain UIMain { get; private set; }

        private readonly IFileRepository _fileRepository;
        private readonly ILogger _logger;
        private readonly DataProcessingParams _dataProcessingParams;

        private readonly HttpDownloader _httpDownloader;
        private readonly WebCrawlerMain _webCrawlerMain;

        private readonly IModelRule _modelRule0;
        private readonly IModelRule _modelRule1;
        private readonly IModelRule _modelRule2;
        private readonly IModelRule _modelRule3;
        private readonly IModelRule _modelRule4;
        private readonly IModelRule _modelRule5;
        private readonly IModelRule _modelRule6;
        private readonly IModelRule _modelRule7;
        private readonly IModelRule _modelRule8;
        private readonly IModelRule _modelRule9;
        private readonly ModelRuleNoMatch _modelRuleNoMatch;
        private readonly BitmapProcessingParams _bitmapProcessingParams;
        private readonly InputCreator _inputCreator;
        private readonly ImageLoader _imageLoader;
        private readonly ImageRecognitionMain _imageRecognitionMain;

        private readonly IProcessData _dataProcessor;

        private readonly InputValidator _inputValidator;
        private readonly CsvUtility _csvUtility;
        private readonly ProcessingContextLoader _contextLoader;

        public Bootstrapper()
        {
            _fileRepository = new FileRepository();
            _logger = CreateLogger();
            _dataProcessingParams = new DataProcessingParams();

            _httpDownloader = new HttpDownloader();
            _webCrawlerMain = new WebCrawlerMain(
                httpDownloader: _httpDownloader,
                measurementPointUtility: new MeasurementPointConfigUtility(
                    timeConverter: new TimeConverter(), 
                    fileRepository: _fileRepository), 
                stringEditUtility: new StringEditUtility());

            _modelRule0 = new ModelRule(0);
            _modelRule1 = new ModelRule(1);
            _modelRule2 = new ModelRule(2);
            _modelRule3 = new ModelRule(3);
            _modelRule4 = new ModelRule(4);
            _modelRule5 = new ModelRule(5);
            _modelRule6 = new ModelRule(6);
            _modelRule7 = new ModelRule(7);
            _modelRule8 = new ModelRule(8);
            _modelRule9 = new ModelRule(9);
            _modelRuleNoMatch = new ModelRuleNoMatch();
            _bitmapProcessingParams = new BitmapProcessingParams();
            _inputCreator = new InputCreator();

            _imageLoader = new ImageLoader(
                fileRepository: _fileRepository,
                bitmapProcessingParams: _bitmapProcessingParams, 
                bitmapProcessor: new BitmapProcessor(
                    crop: new Crop(
                        rect: new Rectangle()),
                    grayscale: new Grayscale(0, 0, 0), 
                    contrastStretch: new ContrastStretch(),
                    resizeBilinear: new ResizeBilinear(1, 1), 
                    bitmapProcessingParams: _bitmapProcessingParams));

            _imageRecognitionMain = new ImageRecognitionMain(
                fileRepository: _fileRepository, 
                bitmapProcessingParams: _bitmapProcessingParams,
                machine: new Machine(
                    inputCreator: _inputCreator,
                    outputCreator: new OutputCreator(),
                    learning: new Learning(),
                    imageLoader: _imageLoader),
                imageRecognizer: new ImageRecognizer(
                    fileRepository: _fileRepository, 
                    imageLoader: _imageLoader, 
                    inputCreator: _inputCreator, 
                    predictionHandler: new PredictionHandler(
                        fileRepository: _fileRepository,
                        firstDigitModelMatcher: new FirstDigitModelMatcher(
                            modelRules: GetFirstModelRules()),
                        secondDigitModelMatcher: new SecondDigitModelMatcher(
                            modelRules: GetSecondModelRules()),
                        logger: _logger)),
                            logger: _logger);

            _dataProcessor = GetDataProcessor();

            _inputValidator = new InputValidator();
            _csvUtility = new CsvUtility(_fileRepository);
            _contextLoader = new ProcessingContextLoader(
                folderChooser: new FolderChooser(
                    fileRepository: _fileRepository,
                    folderInfo: new FolderInfo(),
                    inputValidator: _inputValidator),
                configLoader: new LocatioSettingsLoader(
                    fileRepository: _fileRepository),
                configChooser: new ConfigChooser(
                    fileRepository: _fileRepository,
                    inputValidator: _inputValidator),
                csvUtility: _csvUtility);

            UIMain = new UIMain(GetTasks(), _inputValidator, new ProcessingContext());
        }

        private Dictionary<string,ITask> GetTasks()
        {
            return new Dictionary<string, ITask>
            {
                { TaskName.CreateRepository, new CreateRepositoryTask(new RepositoryCreator(_fileRepository)) },
                { TaskName.WebCrawler, new WebCrawlerTask(_webCrawlerMain) },
                { TaskName.ImageRecognition, new ImageRecognitionTask(_imageRecognitionMain) },
                { TaskName.DataProcessing, new DataProcessingTask(_dataProcessor) },
                { TaskName.CreateCsv, new CreateCsvTask(_csvUtility) },
                { TaskName.CreateJson, new CreateJsonTask(new JsonUtility(_fileRepository)) },
                { TaskName.LoadCsv, new LoadCsvTask(_contextLoader) },
                { TaskName.GetApi, new GetApiTask(new WebApiDataLoader(_httpDownloader, new WebApiConfig())) },
                { TaskName.Exit, new ExitTask() }
            };
        }

        private IProcessData GetDataProcessor()
        {
            var filters = new Filters(_dataProcessingParams, _logger);
            
            return new ProcessData(new IProcessData[]
            {
                new PreLogging(_dataProcessingParams, _logger),
                new DepthRegularizer(_dataProcessingParams, _logger),
                filters,
                new GpsRounder(_dataProcessingParams),
                new DuplicateCombiner(),
                new ShorelineFiller(_dataProcessingParams),
                new LocationGenerator(_dataProcessingParams, _logger),
                filters,
            });
        }

        private ILogger CreateLogger()
        {
            var logger = new LoggerConfiguration()
                .WriteTo.RollingFile("logs\\log.txt")
                .WriteTo.Console()
                .CreateLogger();

            logger.Information("=========================================="); //Separate sessions
            Console.Clear();
            return logger;
        }

        private IEnumerable<IModelRule> GetFirstModelRules()
        {
            return new IModelRule[]
            {
                _modelRule0,
                _modelRule1,
                _modelRule2,
                _modelRule3,
                _modelRule4,
                _modelRule5,
                _modelRuleNoMatch
            };
        }

        private IEnumerable<IModelRule> GetSecondModelRules()
        {
            return new IModelRule[]
            {
                _modelRule0,
                _modelRule1,
                _modelRule2,
                _modelRule3,
                _modelRule4,
                _modelRule5,
                _modelRule6,
                _modelRule7,
                _modelRule8,
                _modelRule9,
                _modelRuleNoMatch
            };
        }
    }
}
