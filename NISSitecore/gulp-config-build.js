var minimist = require("minimist");

var knownOptions = {
    string: "buildEnv",
    boolean: "unicornSerializeOn",
    string: "unicornReachableHost",
    boolean: "clean",
    string: "instanceRoot",       //inditcates where sitecore instance is installed. This is useful if you want to test out the build of some other environemnt besides Debug
    default: {
        buildEnv: "dev",
        unicornSerializeOn: true, //allows dev to turn on/off serialization at build time
        unicornReachableHost: "jairosoft.local",
        clean: true,
        instanceRoot: ""
    }
}

var options = minimist(process.argv.slice(2), knownOptions);
console.log("Building for " + options.buildEnv + " environment");

module.exports = function () {

    var parseInstanceRoot = function (defaultPath) {
        var irLen = Number.parseInt(options.instanceRoot.length, 10);
        var zero = Number.parseInt(0, 10);
        var ret = ((irLen > zero) ? options.instanceRoot : defaultPath);
        return ret;
    }

    // This is the developer configuration.
    var instanceRoot = parseInstanceRoot("C:\\inetpub\\wwwroot\\jairosoft");

    var config = {
        solutionName: "jairosoft",
        buildConfiguration: "Debug",
        runCleanBuilds: options.clean,
        unicornSerializeOn: options.unicornSerializeOn,
        unicornReachableHost: options.unicornReachableHost
    };

    /**
     * Updates properties of the given config1 object based on the given root string
     * @param {} config1 Config object to update
     * @param {} root String representing the base of the sitecore installation
     * @returns {} nada; this just updates the config
     */
    var updateConfigForInstanceRoot = function (config1, root) {

        config1.websiteRoot = root + "\\Website";
        config1.sitecoreLibraries = root + "\\Website\\bin";
        config1.licensePath = root + "\\Data\\license.xml";

        return;
    };

    updateConfigForInstanceRoot(config, instanceRoot);

    //If gulp is ran with an environment arg e.g. gulp --buildEnv=<qa|stage-ce|stage-cd>, we
    // tweak the config as needed.
    // Please also see https://msdn.microsoft.com/en-us/library/ms171455.aspx for a hint about
    // including or excluding certain files from the build. We have an update to the Foundation/Serialization
    // project that excludes some Unicorn configs for non-dev environments
    switch (options.buildEnv) {
        case "qa":
            config.buildConfiguration = "QA"; //note the .net project file has a QA conig defined
            config.unicornSerializeOn = false;
            config.unicornReachableHost = "jairosoft.qa"
            updateConfigForInstanceRoot(config, parseInstanceRoot("C:\\inetpub\\wwwroot\\jairosoft.qa"))
            break;
       
        case "prod":      
            config.buildConfiguration = "Production";
            config.unicornSerializeOn = false;
            config.unicornReachableHost = "jairosoft.sugcon"
            updateConfigForInstanceRoot(config, parseInstanceRoot("C:\\Inetpub\\wwwroot\\jairosoft.sugcon"));
            break;

        case "rel":
            config.buildConfiguration = "Release";
            config.unicornSerializeOn = false;
            config.unicornReachableHost = "jairosoft.rel"
            updateConfigForInstanceRoot(config, parseInstanceRoot("C:\\Inetpub\\wwwroot\\jairosoft.rel"));
            break;
        default:
            console.log("Building for dev environment");
    }

    console.log("gulp config:");
    console.log(config);
    return config;
}
