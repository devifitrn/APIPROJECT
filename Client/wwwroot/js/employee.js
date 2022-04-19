/*================================Cek Get master Data================================*/
$.ajax({
    url: "account/GetRegisteredData",
    success: function (result) {
        console.log(result);
    }
})

/*================================DataTable================================*/
$(document).ready(function () {
    $('.tableEmployee').DataTable({
        
        "filter": true,
        "orderMulti": false,
        dom: "<'row justify-content-between align-items-center'<'col-md-3'l><'d-flex align-items-center justify-content-center'<'mr-6'B><'mt-1'f>>>" +
         "<'row'<'col-md-12'tr>>" +
         "<'row'<'col-md-5'i><'col-md-7'p>>",
        "ajax": {
            "url": "account/Getregistereddata",
            "datatype": "json",
            "dataSrc": ""
            
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
                "data": "nik"
            },
            {
                "data": "fullName"

            },
            {
                "data": "phone",
                "render": function (data, type, row) {
                    return "+62" + row["phone"].substring(1);
                    
                },
            },
            {
                "data": "email"
            },
            {
                "data": "gender"
            },
            {
                "data": "birthdate",
                "render": function (data, type, row) {
                    //return row["birthdate"].split("T")[0];
                   // var dataGet = new Date(row['birthdate']);
                    //console.log(row['birthdate']);
                    return moment(row["birthdate"]).format('ll');
                },
                
    
            },
            {
                "data": "salary",
                "render": function (data, type, row) {
                    //render berfungsi utk membuat column bisa kita manipulasi string nya

                    return "Rp " + row["salary"];
                },
            },
            {
                "data": "nik",
                "render": function (data, type, row) {
                    return `<button class="fa fa-edit" onclick="GetData('${row["nik"]}', '${row["fullName"]}', '${row["birthdate"]}', '${row["email"]}' , '${row["phone"]}' , '${row["salary"]}', '${row["gender"]}')" data-toggle="modal" data-target="#edited" data-bs-toggle="tooltip" data-bs-placement="top" title="Edit"></button>
                                <button class="fa fa-trash" onclick="Delete(${row['nik']})" data-toggle="modal" data-target="" data-bs-toggle="tooltip" data-bs-placement="top" title="Delete"></button>`;
                },
                "autoWidth": true,
                "orderable": false
            }
        ],
        buttons: [
            {
                extend: 'pdfHtml5',
                text: 'PDF',
                titleAttr: 'PDF',
                orientation: 'portrait',
                pageSize: 'A4',
                exportOptions: {
                    columns: [0, 1, 2, 3, 4, 5, 6, 7],
                },
            },
            {
                extend: 'excel',
                text: 'Excel',
                titleAttr: 'Excel',
                exportOptions: {
                    columns: [0, 1, 2, 3, 4, 5, 6, 7],
                },
            },
            {
                extend: 'csv',
                text: 'csv',
                titleAttr: 'csv',
                exportOptions: {
                    columns: [0, 1, 2, 3, 4, 5, 6, 7],
                },
            },
            {
                extend: 'print',
                text: 'print',
                titleAttr: 'print',
                exportOptions: {
                    columns: [0, 1, 2, 3, 4, 5, 6, 7],
                },
            },

        ],
        
    });
});

/*================================For Register================================*/
function Insert() {
    event.preventDefault()
    var obj = new Object(); //sesuaikan sendiri nama objectnya dan beserta isinya
    //ini ngambil value dari tiap inputan di form nya
    obj.FirstName = $("#firstname").val();
    obj.LastName = $("#lastname").val();
    obj.Phone = $("#phone").val();
    obj.Birthdate = $("#birthdate").val();
    obj.Salary = $("#salary").val();
    obj.Email = $("#email").val();
    obj.Gender = $("#gender").val();
    obj.Password = $("#password").val();
    obj.RoleId = $("#roleid").val();
    obj.Degree = $("#degree").val();
    obj.GPA = $("#gpa").val();
    obj.UniversityId = $("#instansi").val();
    console.log(obj);
    //isi dari object kalian buat sesuai dengan bentuk object yang akan di post

    $.ajax({
        url: "Account/register",
        type: "POST",
        /*headers: {
            Accept: 'application/json',
            'Content-Type': 'application/json',
        },*/
        data: obj, //jika terkena 415 unsupported media type (tambahkan headertype Json & JSON.Stringify();)
        //data: JSON.stringify(obj)

    }).done((result) => {
        Swal.fire(
            'Register successfully',
            result.messageResult,
            'Register success'
        )
    }).fail((error) => {
        Swal.fire(
            'Register failed',
            'Register error'
        )
    });
}

/*================================Get DataTable================================*/
/*function GetData(nik) {
    $('#edited')

    $.ajax({
        type: 'GET',
        url: ``,
    })
        .done((response) => {
            const data = response.result
            const [firstname, ...lastname] = data.fullName.split(' ')
            let gender

            data.gender === 'male' ? (gender = 0) : (gender = 1)

            $('#nik').val(nik)
            $('#editfirstname').val(firstname)
            $('#editlastname').val(lastname.join(' '))
            $('#editphone').val(data.phone)
            $('#editbirthdate').val(formatDate(data.birthdate))
            $('#editgender').val(gender)
            $('#editsalary').val(data.salary)
            $('#editemail').val(data.email)
            $('#editdegree').val(data.degree)
            $('#editgpa').val(data.gpa)
        })
        .fail((error) => {
            Swal.fire(
                'failed',
                'error'
            )
        })
}*/

function GetData(nik, fullname, birthdate, email, phone, salary, gender) {

    //var nama = "devi Nurvadila";
    console.log(nik);
    console.log(fullname);
    console.log(birthdate);
    console.log(email);
    console.log(phone);
    console.log(salary);
    console.log(gender);

    const [firstname, ...lastname] = fullname.split(' ');
    var now = new Date(birthdate);
    var day = ("0" + now.getDate()).slice(-2);
    var month = ("0" + (now.getMonth() + 1)).slice(-2);
    var today = now.getFullYear() + "-" + (month) + "-" + (day);
    
    console.log(today);
    $('#nik').val(nik);
    $('#editfirstname').val(firstname);
    $('#editlastname').val(lastname);
    $('#editbirthdate').val(today);
    $('#editemail').val(email);
    $('#editphone').val(phone);
    $('#editsalary').val(salary);
    if (gender == "Male") {
        $('#editgender').val("0");
    } else {
        $('#editgender').val("0");
    }

}

/*================================For Update data================================*/
function Update(nik) {

    event.preventDefault()
   // var nik = $("#nik").val();
    var obj = new Object();
    obj.NIK = $("#nik").val();
    obj.FirstName = $("#editfirstname").val();
    obj.LastName = $("#editlastname").val();
    obj.Phone = $("#editphone").val();
    obj.BirthDate = $("#editbirthdate").val();
    obj.Salary = parseInt($("#editsalary").val());
    obj.Email = $("#editemail").val();
    obj.Gender = parseInt($("input[name=genderedit]:checked").val());
    

    $.ajax({
        url: "employees/UpdateNIK/" +nik,
        type: "PUT",
        data: obj
        /*headers: {
            Accept: 'application/json',
            'Content-Type': 'application/json',
        },
        dataType: 'json', //jika terkena 415 unsupported media type (tambahkan headertype Json & JSON.Stringify();)
        data: JSON.stringify(obj)*/

    }).done((result) => {
        Swal.fire(
            'success',
            'success'
        )
    }).fail((error) => {
        Swal.fire(
            'failed',
            'error'
        )
    });
}

/*=====================================Delete====================================*/
function Delete(key) {
    Swal.fire({
        title: 'Are you Sure?',
        text: "Data will be deleted from the database!",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Delete',
        cancelButtonColor: 'Cancel',
    }).then((result) => {
        if (result.isConfirmed) {
            $.ajax({
                url: "employees/Delete/" + key,
                type: "delete",
                crossDomain: true,
            }).done((result) => {
                Swal.fire(
                    'success',
                    result.messageResult,
                    'success'
                )
                table.ajax.reload();
            }).fail((error) => {
                Swal.fire(
                    'failed',
                    'error'
                )
            })
        }
    })
}

/*================================Chart Gender================================*/
chartForGender();
function chartForGender() {
    $.ajax({
        url: '/account/Getregistereddata',
        success: function (result) {
            console.log(result);
            var text = " ";
            var male = 0;
            var female = 0;
            for (var i = 0; i < result.length; i++) {
                var Genderresult = result[i].gender;
                if (Genderresult == "Male") {
                    male++;
                }
                else {
                    female++;
                }
            }

            var options = {
                series: [male , female],
                chart: {
                    width: 380,
                    type: 'pie',
                    toolbar: {
                        show: true,
                        offsetX: 0,
                        offsetY: 0,
                        tools: {
                            download: true,
                            selection: true,
                            zoom: true,
                            zoomin: true,
                            zoomout: true,
                            pan: true,
                            reset: true | '<img src="/static/icons/reset.png" width="20">',
                            customIcons: []

                        },
                        export: {
                            csv: {
                                filename: undefined,
                                columnDelimiter: ',',
                                headerCategory: 'category',
                                headerValue: 'value',
                                dateFormatter(timestamp) {
                                    return new Date(timestamp).toDateString()
                                }
                            },
                            svg: {
                                filename: undefined,
                            },
                            png: {
                                filename: undefined,
                            }
                        },
                        autoSelected: 'zoom'
                    },
                },
                dataLabels: {
                    enabled: true
                },
                series: [male, female],
                labels: ['Male', 'Female'],
                noData: {
                    text: 'Loading...'
                }
                        
                
            };

            var chart = new ApexCharts(document.querySelector("#chartGender"), options);
            chart.render();
        },
        async: false
    });
}

/*================================Chart University================================*/
chartUniversity();
function chartUniversity() {
    $.ajax({
        url: 'account/Getregistereddata',
        success: function (result) {
            console.log(result);
            var text = " ";
            var universityA = null;
            var universityB = null;
            var universityC = null;
            for (var i = 0; i < result.length; i++) {
                var univResult = result[i].universityName;
                if (univResult == "Universitas Pendidikan Indonesia") {
                    universityA++;
                }
                else if (univResult == "Universitas Padjajaran") {
                    universityB++;
                } else {
                    universityC++;
                }
            }

            var options = {
                series: [{
                    data: [universityA , universityB , universityC]
                }],
                chart: {
                    type: 'bar',
                    height: 380
                },
                plotOptions: {
                    bar: {
                        barHeight: '100%',
                        distributed: true,
                        horizontal: true,
                        dataLabels: {
                            position: 'bottom'
                        },
                    }
                },
                colors: ['#d4526e', '#13d8aa', '#f48024'],
                dataLabels: {
                    enabled: true,
                    textAnchor: 'start',
                    style: {
                        colors: ['#fff']
                    },
                    formatter: function (val, opt) {
                        return opt.w.globals.labels[opt.dataPointIndex] + ":  " + val
                    },
                    offsetX: 0,
                    dropShadow: {
                        enabled: true
                    }
                },
                stroke: {
                    width: 1,
                    colors: ['#fff']
                },
                xaxis: {
                    categories: ['UPI', 'UNPAD', 'ITB'],
                },
                yaxis: {
                    labels: {
                        show: false
                    }
                },
                title: {
                    text: 'University Count',
                    align: 'center',
                    floating: true
                },
                /*subtitle: {
                    text: 'Category Names as DataLabels inside bars',
                    align: 'center',
                },*/
                tooltip: {
                    theme: 'dark',
                    x: {
                        show: false
                    },
                    y: {
                        title: {
                            formatter: function () {
                                return ''
                            }
                        }
                    }
                }
            };

            var chart = new ApexCharts(document.querySelector("#chartUniversity"), options);
            chart.render();
        },
    });
}

/*================================LogIn================================*/

function loginUser() {
    event.preventDefault();
    var logInVM = {
        Email: $("#Email").val(),
        Password: $("#Password").val()
    };
    //console.log(logInVM.JWT);
    $.ajax({
        url: 'https://localhost:44352/login/login',
        type: 'post',
        data: logInVM,
        /*headers : {
            "Authorization": "Bearer " + JWToken,
        }*/
         
    }).done(result => {
        console.log(result)
        if (result.status == 200) {
            Swal.fire({
                icon: 'success',
                title: 'login success',
                showConfirmButton: false,
                timer: 1500
            }).then(function () {
                window.location.href = "Account";
            });
        } else {
            Swal.fire({
                icon: 'error',
                title: 'login failed',
                text: result.message,
            }).then(function () { });
        
        }
    }).fail(error => {
        Swal.fire({
            icon: 'error',
            title: 'Login Gagal, Data Tidak Sesuai!',
            text: error.message,
        });
    });
}