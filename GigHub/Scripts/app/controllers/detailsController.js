//Revealing module pattern
var DetailsController = function (followingService) {
    var button;
    var init = function () {
        $(".js-toogle-follow").click(function (e) {
            button = $(e.target);

            var foloweeId = button.attr("data-user-id");

            if (button.hasClass("btn-info"))
                followingService.stopFollow(foloweeId, done, fail);
            else if (button.hasClass("btn-default"))
                followingService.follow(foloweeId, done, fail);
        });
    };

    var done = function () {
        var text = button.hasClass("btn-default") ? "Following" : "Follow";
        button.toggleClass("btn-default").toggleClass("btn-info").text(text);
    };

    var fail = function () {
        alert("Something failed!");
    };

    return {
        init: init
    }
}(FollowingService);