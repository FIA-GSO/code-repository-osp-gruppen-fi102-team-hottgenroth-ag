/** @type {import('tailwindcss').Config} */
module.exports = {
  content: [
    "./src/**/*.{html,ts}",
  ],
  theme: {
    extend: {},
    screens: {
      'xs': {'min': '0px', 'max': '599px'},
      'sm': {'min': '600px', 'max': '959px'},
      'md': {'min': '960px', 'max': '1279px'},
      'lg': {'min': '1280px', 'max': '1919px'},
      'xl': {'min': '1920px', 'max': '5000px'},
      'lt-sm': {'min': '0px', 'max': '599px'},
      'lt-md': {'min': '0px', 'max': '959px'},
      'lt-lg': {'min': '0px', 'max': '1279px'},
      'lt-xl': {'min': '0px', 'max': '1919px'},
      'gt-xs': '600px',
      'gt-sm': '960px',
      'gt-md': '1280px',
      'gt-lg': '1920px'
    },
  },
  plugins: [],
}

