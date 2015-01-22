/// <reference path="node_modules/grunt/lib/grunt.js" />

// node-debug (Resolve-Path ~\AppData\Roaming\npm\node_modules\grunt-cli\bin\grunt) task:target

module.exports = function (grunt) {
    /// <param name="grunt" type="grunt" />

	grunt.loadNpmTasks('grunt-karma');   
    grunt.loadNpmTasks('grunt-contrib-uglify');
    grunt.loadNpmTasks('grunt-contrib-watch');
    grunt.loadNpmTasks('grunt-contrib-copy');
    grunt.loadNpmTasks('grunt-contrib-clean');
    //grunt.loadNpmTasks('grunt-contrib-jshint');
    //grunt.loadNpmTasks('grunt-contrib-qunit');
    //grunt.loadNpmTasks('grunt-contrib-concat');

    grunt.initConfig({
        staticFilePattern: '**/*.{js,css,map,html,htm,ico,jpg,jpeg,png,gif,eot,svg,ttf,woff}',
        pkg: grunt.file.readJSON('package.json'),
			uglify: {
				options: {
					banner: '/*! <%= pkg.name %> <%= grunt.template.today("dd-mm-yyyy") %> */\n'
				}
			},
			clean: {
				options: { force: false },
				bower: [
					'!src/Mvc/wwwroot/*.html', 
					'src/Mvc/wwwroot/Content', 
					'src/Mvc/wwwroot/fonts', 
					'src/Mvc/wwwroot/Scripts/*.{js,map}', 
					'!src/Mvc/wwwroot/Scripts/gs-*.js',
					'!test/Mvc/wwwroot/',
					'test/Mvc/wwwroot/*.*',
					'test/Mvc/wwwroot/Content',
					'test/Mvc/wwwroot/fonts',
					'test/Mvc/wwwroot/Scripts/*.{js,map}', 
					'!test/Mvc/wwwroot/Scripts/gs-*-test.js'
				],
			},
			karma: {
				unit: {
					configFile: 'karma.conf.js'
				}
			},
			copy: {
				// This is to work around an issue with the dt-angular bower package https://github.com/dt-bower/dt-angular/issues/4
				fix: {
					files: {
						"bower_components/jquery/jquery.d.ts": ["bower_components/dt-jquery/jquery.d.ts"]
					}
				},
				bower: {
					files: [
						{   // JavaScript
							expand: true,
							flatten: true,
							cwd: "bower_components/",
							src: [
								"jquery/dist/*.{js,map}",
								"bootstrap/dist/**/*.js",
								"angular/*.{js,.js.map}",
								"angular-route/*.{js,.js.map}"
							],
							dest: 'src/Mvc/wwwroot/Scripts/',
							options: { force: true }
						},
						{   // CSS
							expand: true,
							flatten: true,
							cwd: "bower_components/",
							src: [
								"bootstrap/dist/**/*.css",
							],
							dest: 'src/Mvc/wwwroot/Content/',
							options: { force: true }
						},
						{   // Fonts
							expand: true,
							flatten: true,
							cwd: "bower_components/",
							src: [
								"bootstrap/**/*.{woff,svg,eot,ttf}",
							],
							dest: 'src/Mvc/wwwroot/fonts/',
							options: { force: true }
						}
					]
				}
			},
			watch: {
				dev: {
					files: ['bower_components/<%= staticFilePattern %>', 'Client/<%= staticFilePattern %>'],
					tasks: ['dev']
				},
				karma: { //run unit tests with karma (server needs to be already running)
					unit : {
						configFile: 'karma.conf.js'
					},
					//files: ['src/Mvc/wwwroot/Scripts/**/*.js', 'test/Mvc/wwwroot/Scripts/**/*.js'],
					//exclude: ['src/Mvc/wwwroot/Scripts/**/*.min.js'],
					tasks: ['karma:unit:run'] //NOTE the :run flag
				}
			}
	});

    grunt.registerTask('dev', ['clean', 'copy']);
    grunt.registerTask('default', ['dev']);
	grunt.registerTask('jstest', ['karma']);
};