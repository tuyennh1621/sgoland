/**
 * To find your Firebase config object:
 * 
 * 1. Go to your [Project settings in the Firebase console](https://console.firebase.google.com/project/_/settings/general/)
 * 2. In the "Your apps" card, select the nickname of the app for which you need a config object.
 * 3. Select Config from the Firebase SDK snippet pane.
 * 4. Copy the config object snippet, then add it here.
 */
//const config = {
//  apiKey: "AIzaSyAfbRToAYeUc-yB6NuWpPND70N4QdpZsz8",
//  authDomain: "friendlychat-f9242.firebaseapp.com",
//  databaseURL: "https://friendlychat-f9242-default-rtdb.firebaseio.com",
//  projectId: "friendlychat-f9242",
//  storageBucket: "friendlychat-f9242.appspot.com",
//  messagingSenderId: "165780709589",
//  appId: "1:165780709589:web:15806a795ef8ec9af6025c"
//};

const config = {
    apiKey: "AIzaSyCgYLUomIjhs4uA2imn5GZpF3YgyJp8hS8",
    authDomain: "gostay-1ae07.firebaseapp.com",
    projectId: "gostay-1ae07",
    storageBucket: "gostay-1ae07.appspot.com",
    messagingSenderId: "565579751675",
    appId: "1:565579751675:web:08ff3773f89159a438905e",
    measurementId: "G-NK9LLCYW8H"
};

export function getFirebaseConfig() {
  if (!config || !config.apiKey) {
    throw new Error('No Firebase configuration object provided.' + '\n' +
    'Add your web app\'s configuration object to firebase-config.js');
  } else {
    return config;
  }
}