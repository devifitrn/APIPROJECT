// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

/*const animals = [
    { name: "garfield", species: "cat", class: { name: "mamalia" } },
    { name: "nemo", species: "fish", class: { name: "Pisces" } },
    { name: "tom", species: "cat", class: { name: "mamalia" } },
    { name: "garry", species: "cat", class: { name: "mamalia" } },
    { name: "dory", species: "fish", class: { name: "Pisces" } },
    { name: "dolphin", species: "fish", class: { name: "Pisces" } },
    { name: "Meow", species: "cat", class: { name: "mamalia" } },
    { name: "carfish", species: "fish", class: { name: "Pisces" } },
    { name: "brownie", species: "cat", class: { name: "mamalia" } },
    { name: "Guppy", species: "fish", class: { name: "Pisces" } }
];*/

/*let onlyCat = [];
for (var i = 0; i < animals.length; i++) {
    if (animals[i].species == "cat") {
        onlyCat.push(animals[i]);
    }
}
console.log(onlyCat);

let changeFish = [];
for (var i = 0; i < animals.length; i++) {
    if (animals[i].species == "fish") {
        animals[i].class.name = "non mamalia";
        changeFish.push(animals[i]);
    }
}
console.log(changeFish);
console.log(animals);*/
/*$(document).ready(function (result) {
    $('#example').DataTable({
        "ajax": {
            url: '//cdn.datatables.net/1.11.5/js/jquery.dataTables.min.js'
        },
        success: function (result) {
            console.log(result.results);
            var text = "";
            $.each(result.results, function (key, val) {
                "column": [
                { data: 'no' },
                { data: 'name' },
                { data: 'detail' }
            ]
            });
            console.log(text);
            $("#example").html(text);
            *//*"column": [*//*
                { data: 'no' }
                { data: 'name' }
                { data: 'detail'}
            ]*/
        /*}
    });*/
    $.ajax({
        url: "https://pokeapi.co/api/v2/pokemon?limit=100&offset=200",
        success: function (result) {
            //image
           
            console.log(result.results);
            var text = "";
            $.each(result.results, function (key, val) {
                text += `<tr>
                            <td>${key + 1}</td>
                            <td>${val.name}</td>
                            <td><button class="btn btn-primary" data-toggle="modal" onclick ="testing('${val.url}')" data-target="#detailModal">Detail</button></td>
    
                        </tr>`;
            });
            console.log(text);
            $("#dataPokemon").html(text);
    
        }
        
            
    })

$(document).ready(function () {
    $('#example').DataTable({
        "dom": '<"toolbar">frtip',
        
    });

    $("div.toolbar").html('<h3>Pokemon Table</h3>');
    $('#example tbody').on('click', 'tr', function () {
        $(this).toggleClass('selected');
    });

    $('#button').click(function () {
        alert(table.rows('.selected').data().length + ' row(s) selected');
    });
});

    function testing(url) {
        $.ajax({
            url: url,
            success: function (result) {
                //image 
                $('.img-pokemon').attr(
                    'src',
                    `${result.sprites.other['official-artwork'].front_default}`
                )

                //name 
                // $('.species-name').html(capitalize(result.name))
                $('.species').html(result.name)
                $('.sizew').html(result.weight)
                $('.sizeh').html(result.height)
                //$('.status').html(result.stat)

                $('.hp').attr('style', `width:${result.stats[0].base_stat}%`)
                $('.hp').html(`Heat Point: ${result.stats[0].base_stat}%`, `${result.stats[0].base_stat}`)

                $('.attack').attr('style', `width:${result.stats[1].base_stat}%`)
                $('.attack').html(`Attack :  ${result.stats[1].base_stat}%`, `${result.stats[1].base_stat}`)

                $('.defense').attr('style', `width:${result.stats[2].base_stat}%`)
                $('.defense').html(`Defense :  ${result.stats[2].base_stat}%`, `${result.stats[2].base_stat}`)

                $('.speed').attr('style', `width:${result.stats[5].base_stat}%`)
                $('.speed').html(`Speed :  ${result.stats[5].base_stat}%`, `${result.stats[5].base_stat}`)

                let text = "";
                $.each(result.abilities, function (key, val) {
                    $('.ability').html(val.ability.name);
                });

                $.each(result.stats, function (key, val) {
                    $('.status').html(val.stat.name);
    
                });

                $.each(result.types, function (key, val) {
                    $('.type').html(val.type.name);
                });
                // text += `<span class="badge badge-pill badge-info" style="font-size: 15px; margin: auto">${result.name}</span>`;
                console.log(result);

                // $.each('modal-body').html(result.name);
            }
        });
    }; 
/*$(document).ready(function () {
    $('#example').DataTable({
        "filter": true,
        "orderMulti": false,
        "ajax": {
            "url": "https://pokeapi.co/api/v2/pokemon?limit=100&offset=200",
            "datatype": "json",
            "dataSrc": "results"
        },
        "columns": [
            {
                "data": null,
                "name": "no",
                "autoWidth": true,
                "render": function (data, type, row, meta) {
                    return meta.row +1;
                }
            },
            {
                "data": "name"
            },
            {
                "data": null,
                "render": function (data, type, row) {
                    //render berfungsi utk membuat column bisa kita manipulasi string nya

                    return `<button class="btn btn-primary" data-toggle="modal" onclick ="testing('${row.url}')" data-target="#detailModal">Detail</button>`;
                },
                "autoWidth": true
            }
        ]
    });
});
*/


    