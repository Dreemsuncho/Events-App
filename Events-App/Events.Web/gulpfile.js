
let gulp = require('gulp');

gulp.task('inject', function () {
	let wiredep = require('wiredep').stream;
	let inject = require('gulp-inject');

	let injectSrc = gulp.src(['./wwwroot/Styles/*.css', './wwwroot/Scripts/*.js'], { read: false });
	let injectOptions = { ignorePath: "wwwroot" };

	let options = {
		bowerJson: require('./bower.json'),
		directory: './wwwroot/bower',
		ignorePath: '../../wwwroot'
	};

	return gulp.src('./Views/Shared/*.cshtml')
		.pipe(wiredep(options))
		.pipe(inject(injectSrc, injectOptions))
		.pipe(gulp.dest('./Views/Shared'));
});