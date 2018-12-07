import {app, BrowserWindow} from 'electron';
import path from 'path';
import {format as formatUrl} from 'url';

const isDevelopment = process.env.NODE_ENV !== 
'production'

app.on('ready', () => {
    let mainWindow = new BrowserWindow({
        width: 1024
    });
    if(isDevelopment){
        mainWindow.loadURL(`http://localhost:${process.env.ELECTRON_WEBPACK_WDS_PORT}`)
    }else{
        mainWindow.loadURL(formatUrl({
            pathname: path.join(__dirname, 'index.html'),
            protocol: 'file',
            slashes: true
        }))
    }
})

app.on('window-all-closed', () => {
    if (process.platform !== 'darwin') {
        app.quit()
    }
})