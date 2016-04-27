var gulp = require('gulp');
// var shell = require('gulp-shell')
var path = require('path');
var shell = require('shelljs');
var runSequence = require('gulp-run-sequence');

var solution = 'Prismatic.sln';
var basePath = '..';
var solutionPath = path.join(basePath, solution);
var buildConfig = '/t:Build /p:Configuration=Release';
var cleanConfig = '/t:Clean';
var buildTool = 'msbuild';

var isWin = /^win/.test(process.platform);
if (!isWin) {
  buildTool = 'xbuild';
}

var buildCommand = buildTool +' '+ solutionPath + ' ' + buildConfig;
var cleanCommand = buildTool +' '+ solutionPath + ' ' + cleanConfig;

gulp.task('build', ['buildSolution']);

gulp.task('default', function() {
  console.log('Hello World!');
});

gulp.task('buildSolution', ['cleanSolution'], function() {  
  shell.exec(buildCommand, function(code, stdout, stderr) {
    
    if (code > 0) {
      console.log('Exit code:', code);
      console.log('Program output:', stdout);
      console.log('Program stderr:', stderr);  
    }
  });
});

gulp.task('cleanSolution', function() {  
  shell.exec(cleanCommand, function(code, stdout, stderr) {
    if (code > 0) {
      console.log('Exit code:', code);
      console.log('Program output:', stdout);    
      console.log('Program stderr:', stderr);  
    }    
  });
});