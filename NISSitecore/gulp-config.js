module.exports = function () {
    var instanceRoot = "C:\\inetpub\\wwwroot\\sc93sc.dev.local";
  var config = {
    websiteRoot: instanceRoot + "\\",
    sitecoreLibraries: instanceRoot + "\\bin",
    licensePath: instanceRoot + "\\App_Data\\license.xml",    
    solutionName: "NISSitecore",
    buildConfiguration: "Debug",
    buildToolsVersion: '16.0',
    buildMaxCpuCount: 0,
    buildVerbosity: "minimal",
    buildPlatform: "Any CPU",
    publishPlatform: "AnyCpu",
    runCleanBuilds: false
  };
  return config;
}
