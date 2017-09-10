$(function () {
    $(".table").on('click', '.resolve', function () {
        var issueId = $(this).data('id');
        var orderId = $(this).data('order-id');
        console.log(issueId);
        console.log(orderId);
        $.post('/home/MarkResolved', { issueId: issueId }, function () {
            $(".table tr:gt(0)").remove();
            $.get('/home/GetIssues', {orderId: orderId}, function (result) {
                console.log(result);
                result.forEach(function (i) {
                    if(!i.resolved){                  
                        $(".table").append(`<tr><td>${i.note}</td><td><button class="btn btn-danger resolve" data-id="${i.id}" data-order-id="${i.order.id}">Mark Resolved</button></td></tr>`);
                            }
                
            });
        });
        });
    });
});