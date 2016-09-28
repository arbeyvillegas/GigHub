//Revealing module pattern
var FollowingService = function () {
    var follow = function (followeeId, done, fail) {
        $.post("/api/following", { followeeId: followeeId })
                    .done(done)
                    .fail(fail);
    };

    var stopFollow = function (followeeId, done, fail) {
        $.ajax({
            url: "/api/following/" + followeeId,
            method: "DELETE"
        })
                   .done(done)
                   .fail(fail);
    };


    return {
        follow: follow,
        stopFollow: stopFollow
    };
}();