/*
This file is the main entry point for defining Gulp tasks and using Gulp plugins.
Click here to learn more. https://go.microsoft.com/fwlink/?LinkId=518007
*/

var gulp = require('gulp');

var dirs = {
    dest: './wwwroot/lib'
};

gulp.task('default', function () {
    // place code for your default task here
});
gulp.task("copyfiles",
    function() {
        gulp.src("./node_modules/vue/dist/vue.min.js").pipe(gulp.dest(dirs.dest));
    })