var gulp = require('gulp');
var sass = require('gulp-sass')(require('sass'));

gulp.task('sass', function(){
    return gulp.src('sass/style.scss')
        .pipe(sass({outputStyle: 'compressed'}))
        .pipe(gulp.dest('css'))
});

gulp.task('watch', function(){
    gulp.watch('sass/*.scss', gulp.series('sass'));
});