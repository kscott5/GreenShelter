/// <reference path="node_modules/grunt/lib/grunt.js" />

// node-debug (Resolve-Path ~\AppData\Roaming\npm\node_modules\grunt-cli\bin\grunt) task:target

module.exports = function (grunt) {
    /// <param name="grunt" type="grunt" />
    
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
				bower: ['!wwwroot/*.html', 'wwwroot/Content', 'wwwroot/fonts', 'wwwroot/Scripts/*.{js,map}', '!wwwroot/Scripts/gs-*.js'],
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
							dest: "wwwroot/Scripts/",
							options: { force: true }
						},
						{   // CSS
							expand: true,
							flatten: true,
							cwd: "bower_components/",
							src: [
								"bootstrap/dist/**/*.css",
							],
							dest: "wwwroot/Content/",
							options: { force: true }
						},
						{   // Fonts
							expand: true,
							flatten: true,
							cwd: "bower_components/",
							src: [
								"bootstrap/**/*.{woff,svg,eot,ttf}",
							],
							dest: "wwwroot/fonts/",
							options: { force: true }
						}
					]
				}
			},
			watch: {
				dev: {
					files: ['bower_components/<%= staticFilePattern %>', 'Client/<%= staticFilePattern %>'],
					tasks: ['dev']
				}
			}
    });

    grunt.registerTask('dev', ['clean', 'copy']);
    grunt.registerTask('default', ['dev']);
};