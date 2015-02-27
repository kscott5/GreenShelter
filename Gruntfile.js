
/// <reference path="node_modules/grunt/lib/grunt.js" />

// node-debug (Resolve-Path ~\AppData\Roaming\npm\node_modules\grunt-cli\bin\grunt) task:target

module.exports = function (grunt) {
    /// <param name="grunt" type="grunt" />

	grunt.loadNpmTasks('grunt-contrib-uglify');
    grunt.loadNpmTasks('grunt-contrib-watch');
    grunt.loadNpmTasks('grunt-contrib-copy');
    grunt.loadNpmTasks('grunt-contrib-clean');

    grunt.initConfig({
        staticFilePattern: '**/*.{js,css,map,html,htm,ico,jpg,jpeg,png,gif,eot,svg,ttf,woff}',
        pkg: grunt.file.readJSON('package.json'),
			// TODO: Uglify whatever I can...
			uglify: {
				options: {
					banner: '/*! <%= pkg.name %> <%= grunt.template.today("dd-mm-yyyy") %> */\n'
				}
			},
			clean: {
				options: { force: false },
				bower: [
					'!src/Mvc/wwwroot/*.html', 
					'!src/Mvc/wwwroot/images',
					'src/Mvc/wwwroot/Content/*.css', 
					'!src/Mvc/wwwroot/Content/gs-*.css', 
					'!src/Mvc/wwwroot/Content/responsive.css', 
					'src/Mvc/wwwroot/fonts', 
					'src/Mvc/wwwroot/images/prettyPhoto',
					'src/Mvc/wwwroot/Scripts/*.{js,map}', 
					'!src/Mvc/wwwroot/Scripts/gs-*.{js,json}',
					'!src/Mvc/wwwroot/Scripts/modernizr.js',  // JavaScript library that detects HTML5 and CSS3 features in the user’s browser. https://github.com/Modernizr/Modernizr
					'test/Mvc/wwwroot'
				],
			}, // end clean
			// wwwrootcopy: {
				// bower: {
					// files: [
						// {   // src/Mvc
							// expand: true,
							// flatten: false,
							// cwd: "src/Mvc/wwwroot",
							// src: [
								// "**/*.*"
							// ],
							// dest: 'test/Mvc/wwwroot/',
							// options: { force: true }
						// }
					// ]
				// } // end bower
			// }, // end wwwrootCopy
			copy: {
				// This is to work around an issue with the dt-angular bower package https://github.com/dt-bower/dt-angular/issues/4
				fix: {
					files: {
						"bower_components/jquery/jquery.d.ts": ["bower_components/dt-jquery/jquery.d.ts"]
					}
				}, // end fix
				bower: {
					files: [
						{   // JavaScript
							expand: true,
							flatten: true,
							cwd: "bower_components/",
							src: [
								"html5shiv/dist/html5shiv.min.js",
								"respond/dist/html5shiv.min.js",
								
								"jquery/dist/*.min.{js,map}",
								"jquery.validation/dist/jquery.validate.min.js",
								"isotope/dist/isotope.pkgd.min.js",
								"jquery-prettyPhoto/js/jquery.prettyPhoto.js",
								"bootstrap/dist/js/bootstrap.min.js",
								
								"angular/angular.min.{js,js.map}",
								"angular-local-storage/dist/angular-local-storage.min.js",
								"angular-resource/angular-resource.min.{js,js.map}",
								"angular-route/angular-route.min.{js,js.map}",
								
								"wowjs/dist/wow.min.js" // Javascript CSS animation library that triggers animate.css by default
							],
							dest: 'src/Mvc/wwwroot/Scripts/',
							options: { force: true },
							rename: function(dest, src) {
								if(src.indexOf('.pkgd.') > 0)
									src = src.replace('.pkgd.', '.');
								
								if(src.indexOf('isotope') == 0)
									src = src.replace('isotope.', 'jquery.isotope.');
								
								return dest + src;
							}
						},
						{   // CSS
							expand: true,
							flatten: true,
							cwd: "bower_components/",
							src: [
								"bootstrap/dist/css/bootstrap.min.css",
								"font-awesome-bower/css/font-awesome.css", // Iconic font and CSS framework with over 519 pictographic icons
								"animate-css/animate.min.css", // Cross-browser CSS animations library that's wow.js default animation CSS
								"jquery-prettyPhoto/css/prettyPhoto.css"
							],
							dest: 'src/Mvc/wwwroot/Content/',
							options: { force: true },
							rename: function(dest, src) {
								if(src.indexOf('prettyPhoto.css') == 0)
									src = 'jquery.'.concat(src);
								
								return dest + src;
							}
						},
						{   // Fonts
							expand: true,
							flatten: true,
							cwd: "bower_components/",
							src: [
								"bootstrap/dist/fonts/*.{woff,woff2,svg,eot,ttf}",
								"font-awesome-bower/fonts/*.{woff,woff2,svg,eot,ttf}"
							],
							dest: 'src/Mvc/wwwroot/fonts/',
							options: { force: true }
						}
					]
				} // end bower
			}, // end copy
			watch: {
				dev: {
					files: ['bower_components/<%= staticFilePattern %>', 'Client/<%= staticFilePattern %>'],
					tasks: ['dev']
				}
			}
	});

    grunt.registerTask('dev', ['clean', 'copy']);
	grunt.registerTask('test', ['dev', 'wwwrootcopy']);
    grunt.registerTask('default', ['dev']);
};