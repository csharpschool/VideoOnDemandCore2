function videoJS(video) {
    var container = document.getElementById("video");

    videojs(container, {
        controls: true,
        techOrder: ["youtube"],
        sources: [{ type: "video/youtube", src: video }]
    }, function () {
    });
}
