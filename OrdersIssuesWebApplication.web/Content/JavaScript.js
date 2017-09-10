$(function () {
    $(".complete").on('click', function () {
        var Id = $(this).data('id');
        console.log(Id);
        $.post('/home/MarkComplete', { orderId: Id }, function () {
            $(".table tr:gt(0)").remove();
            $.get('/home/GetOrders', function (result) {
                console.log(result);
                result.forEach(function (o) {
                    $(".table").append(`<tr id = '${o.id}'><td>${o.title}</td><td>${o.date}</td><td>${o.amount}</td><td>${o.Issues.filter(i => i.Resolved).length}</td><td>${o.Issues.filter(i => !i.Resolved).length}</td></tr>`);
                    if (o.Issues.filter(i => !i.Resolved).length != 0)
                     {
                        $(`#${o.id}`).append(`<td><button disabled class='btn btn-danger'>Mark Complete</button></td>`);
                     }
                    else
                    {
                        $(`#${o.id}`).append(`<td><button class='btn btn-danger complete' data-id='${o.Id}'>Mark Complete</button></td>`);
                    }
                    $(`#${o.id}`).append(`<td><a class='btn btn-danger' href=''>See Details</a></td>`);
                });
            });
        });
    });
});