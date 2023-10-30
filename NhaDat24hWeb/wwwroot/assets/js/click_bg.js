var clicked = true;
var idhide = '';
function bg_act(id1, id2) {
    
    if (idhide != '') {
        document.getElementById(idhide).style.maxWidth = "40px";
    }
    document.getElementById(id1).style.maxWidth = "inherit";

    if (clicked) {
        document.getElementById(id2).style.cssText = "animation: background-fade 2s;";
        clicked = false;
    }
    else {
        document.getElementById(id2).style.cssText = "animation: background-fade1 2s;";
        clicked = true;
    }
    idhide = id1;
}

