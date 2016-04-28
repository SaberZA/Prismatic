var gulp = require('gulp');
// var shell = require('gulp-shell')
var path = require('path');
var shell = require('shelljs');
var runSequence = require('gulp-run-sequence');
var async = require('async');

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

var buildCommand = buildTool + ' ' + solutionPath + ' ' + buildConfig;
// buildCommand = 'echo buildTask';
var cleanCommand = buildTool + ' ' + solutionPath + ' ' + cleanConfig;

gulp.task('build', ['buildSolution']);

gulp.task('default', function() {
    console.log('Hello World!');
});

gulp.task('buildSolution', function() {
    async.series([
        function() {
            shell.exec(buildCommand, {
                async: false
            });
        },
        function() {
            console.log('after msbuild...');
        }
    ]);
});

gulp.task('cleanSolution', function() {
    shell.exec(cleanCommand, {
        async: false
    });
});
