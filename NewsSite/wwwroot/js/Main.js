$(function () {
    var dropDownContents;
    var selectUser = $("#selectUser").html();

    getAllUsersForDropDown();


    function getAllUsersForDropDown() {
        $.ajax({
            url: '/api/users/getallusers',
            method: 'GET',
        })
            .done(function (result) {
                var concatinatedResult = "";

                $.each(result, function (index, item) {
                    concatinatedResult += '<option value=' + item.email + '>' + item.userName+'</option>';
                });

                $("#selectUser").html(concatinatedResult);
            })
    }
});

$("#loginUser").click(function () {
    let username = $("#selectUser").val();

    $.ajax({
        url: '/api/users/login',
        method: 'POST',
        data: { email: username }
    })
        .done(function (result) {
            $("#loggedIn").text(result);
        });

});

$("#checkCanSeeOpen").click(function () {
    $.ajax({
        url: '/api/users/open',
        method: 'GET'
    }).done(function (result) {
        console.log(result);
    })
});

$("#checkCanSeeHidden").click(function () {
    $.ajax({
        url: '/api/users/hiddennews',
        method: 'GET'
    }).done(function (result) {
        console.log(result);
    })
});

$("#checkCanSeeHiddenTwentyYears").click(function () {
    $.ajax({
        url: '/api/users/age',
        method: 'GET'
    }).done(function (result) {
        console.log(result);
    })
});

$("#checkCanPublishSports").click(function () {
    $.ajax({
        url: '/api/users/sport',
        method: 'GET'
    }).done(function (result) {
        console.log(result);
    })
});

$("#checkCanPublishCulture").click(function () {
    $.ajax({
        url: '/api/users/culture',
        method: 'GET'
    }).done(function (result) {
        console.log(result);
    })
});

$("#recreateAllUsers").click(function () {
    $.ajax({
        url: '/api/users/recoverusers',
        method: 'GET'
    }).done(function (result) {
        console.log(result);
    })
});

$("#showAllUsersWithClaims").click(function () {
    $.ajax({
        url: '/api/users/getalluserswithclaims',
        method: 'GET'
    }).done(function (result) {
        console.log(result);
    })
});



