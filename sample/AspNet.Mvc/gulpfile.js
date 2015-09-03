/// <binding AfterBuild='copy' Clean='clean' />

var gulp = require("gulp"),
  rimraf = require("rimraf"),
  fs = require("fs");

eval("var project = " + fs.readFileSync("./project.json"));

var paths = {
  bower: "./bower_components/",
  lib: "./" + project.webroot + "/lib/"
};

gulp.task("clean", function (cb) {
  rimraf(paths.lib, cb);
});

gulp.task("copy", ["clean"], function () {
  var bower = {
    "bootstrap": "bootstrap/dist/**/*.{js,map,css,ttf,svg,woff,eot}",
    "jquery": "jquery/jquery*.{js,map}",
  }

  for (var destinationDir in bower) {
    gulp.src(paths.bower + bower[destinationDir])
      .pipe(gulp.dest(paths.lib + destinationDir));
  }

  var linkedFiles = {
      "Views/Shared/Components/DnxFlash/": "../../src/DnxFlash.AspNet.Razor.ViewHelpers/Views/Shared/Components/DnxFlash/*.cshtml"
  };

  for (var linkedFile in linkedFiles) {
      gulp.src('./' + linkedFiles[linkedFile])
        .pipe(gulp.dest('./' + linkedFile));
  }
});
