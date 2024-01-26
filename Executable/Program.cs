using PdfExtractor.Services;

TableProccessor tableProcessor = TableProccessor.Instance;
var SOURCE_PATH = Directory.GetCurrentDirectory().ToString();
tableProcessor.ProccessTable(SOURCE_PATH + "/Requirements.pdf");