/** @type {import('tailwindcss').Config} */
module.exports = {
  mode: 'jit',
  content: [
    "./src/**/*.{html,ts,scss}",
    "./src/app/shared/components/**/*.{html,ts,scss}"

  ],
  theme: {
    extend: {
      colors: {
        koala: '#C1C1C1'
      },
      fontSize: {
        'dynamic': 'clamp(0.875rem, 1vw + 0.5rem, 1.25rem)'
      }
    },
  },
  plugins: [],
}

