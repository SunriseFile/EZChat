const VueLoaderPlugin = require('vue-loader/lib/plugin')
module.exports = {
    mode: 'development',
    module: {
        rules: [
            {
                test: /\.vue$/,
                loader: 'vue-loader'
                // options: {
                //     loaders: {
                //         scss: 'vue-style-loader!css-loader!sass-loader', // <style lang="scss">
                //     },
                // }
            }
        ]
    },
    plugins: [
        new VueLoaderPlugin()
    ]
}
