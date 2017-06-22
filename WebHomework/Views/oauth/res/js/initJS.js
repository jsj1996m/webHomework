/**
 * Created by jsj1996m on 2016/12/23.
 */
var nowSeason = 1;

$(document).ready(function () {
    $('.parallax').parallax();
    myVid=document.getElementById("myVideo");
    myVid.volume=0;
    //turnSeason();
    //setTimeout(turnSeason,1000);
});
window.onscroll = function () {
    //document.body.style.backgroundPositionY = (-window.pageYOffset / speed) + "px";
}


function turnSeason() {
    var nextSeason = nowSeason % 4 + 1;
    nowSeason = nextSeason;
    $("#season" + nowSeason).fadeOut(500);
    console.log("sss"+nowSeason);
    //$("#season" + nextSeason).fadeIn(500);
    setTimeout(turnSeason,1000);
}




