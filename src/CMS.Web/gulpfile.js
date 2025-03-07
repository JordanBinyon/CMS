const gulp = require("gulp");
const sass = require("gulp-sass")(require("sass"));
const cleanCSS = require("gulp-clean-css");

gulp.task("scss", function () {
  return gulp
    .src("wwwroot/scss/main.scss") // Source file
    .pipe(sass().on("error", sass.logError)) // Compile SCSS to CSS
    .pipe(cleanCSS()) // Minify the CSS
    .pipe(gulp.dest("wwwroot/css")); // Output to wwwroot/css
});

gulp.task("watch", function () {
  gulp.watch("wwwroot/scss/**/*.scss", gulp.series("scss")); // Watch for changes
});

gulp.task("default", gulp.series("scss"));
