var urlAlteracao = '/Beneficiario/Alterar/';

$(document).ready(function () {

    $.ajaxSetup({ cache: false });

    $('#modalCreate').on('shown.bs.modal', function () {

        setTimeout(function () {
            find();
            $('#formBeneficiario #IdCliente').val(localStorage.getItem('idCliente'));
           
        }, 1000);


        $('#formBeneficiario #btnIncluir').on('click', function (e) {
            e.preventDefault();
            NovoBeneficiario();
        });

        
    });
});

function find() {
    if (document.getElementById("gridBeneficiarios"))
        $('#gridBeneficiarios').jtable({
            title: '',
            paging: true, //Enable paging
            pageSize: 5, //Set page size (default: 10)
            sorting: true, //Enable sorting
            defaultSorting: 'Nome ASC', //Set default sorting
            actions: {
                listAction: '/Beneficiario/BeneficiarioList'
            },
            fields: {
                Cpf: {
                    title: 'CPF',
                    width: '35%',
                    displayFormat: '000.000.000-00'
                },
                Nome: {
                    title: 'Nome',
                    width: '50%'
                },
                Acao: {
                    title: '',
                    display: function (data) {
                        return `<div class="acao"> 
                                    <button onclick="RenderActionsAlterar(${data.record.Id})" class="btn btn-primary btn-sm btn-alterar">Alterar</button>
                                    <button onclick="openConfirmsSwal(${data.record.Id})" class="btn btn-danger btn-sm btn-excluir">Excluir</button>
                                </div>
                        ` ;
                    }
                }
            }
        });

    if (document.getElementById("gridBeneficiarios"))
        $('#gridBeneficiarios').jtable('load');
}

function NovoBeneficiario() {

    $.ajax({
        url: '/Beneficiario/Incluir',
        method: "POST",
        data: {
            "Cpf": $("#formBeneficiario #Cpf").val(),
            "NOME": $("#formBeneficiario #Nome").val(),
            "IdCliente": $("#formBeneficiario #IdCliente").val()
        },
        error:
            function (r) {
                if (r.status == 400)
                    ModalDialog("Ocorreu um erro", r.responseJSON);
                else if (r.status == 500)
                    ModalDialog("Ocorreu um erro", "Ocorreu um erro interno no servidor.");
            },
        success:
            function (r) {
                ModalDialog("Sucesso!", r);
                $("#formCadastro")[0].reset();
                find();
            }
    });

}

function limparCampos() {
    $('#formBeneficiario input').val('');
    $('#formBeneficiario #Cpf').focus();
}

function RenderActions(renderActionstring) {
    $("#OpenDialog").load(renderActionstring);
};

function RenderActionsAlterar(id) {
    var url = `${urlAlteracao}${id}`;
    CarregarComboCliente();
    $("#OpenDialog").load(url);
};

function DeletarBeneficiario(id) {

    $.ajax({
        url: '/Beneficiario/Deletar',
        data: { 'id': id },
        type: 'POST',
        success: function (r) {
            ModalDialog("Sucesso!", r);
            find();
        },
        error: function (err) { alert("Error: " + err.responseText); }
    });
};

function AlterarBeneficiario(id) {

    $.ajax({
        url: '/Beneficiario/Alterar/',
        type: 'POST',
        data: { 'id': id },
        success: function (response) {
            cons
        },
        error: function (err) { alert("Error: " + err.responseText); }
    });
};

function Clean() {
    $('#modalCreate').find('textarea,input').val('');
};

function openConfirmsSwal(id) {
    Swal.fire({
        title: 'Excluir a Beneficiario',
        text: 'Você tem certeza de Excluir o Beneficiario?',
        type: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Sim, exclua!',
        cancelButtonText: 'Não, Cancelar!'
    }).then((result) => {
        if (result.value) {
            DeletarBeneficiario(id);
        }
    });
}


function validarCpf(cpf) {
    //retira caracteres especiais
    if (cpf != undefined || cpf != null) {
        cpf = cpf.replace(/[^a-z0-9]/gi, "");
        if (cpf.length == 11) {
            //verificar cpfs comuns
            let cpfsComuns = ["00000000000", "11111111111", "22222222222", "33333333333", "44444444444", "55555555555", "66666666666", "77777777777", "88888888888", "99999999999"];
            if (cpfsComuns.find(item => item == cpf) != undefined) {
                return false;
            }
            //verificadores
            let mult = 0;
            let cont = 0;
            //guardar digitos
            const dig1 = cpf.charAt(9);
            const dig2 = cpf.charAt(10);
            //multiplicar de 10 a 2
            for (let i = 10; i >= 2; i--) {
                mult += (cpf.charAt(cont) * i);
                cont++;
            }
            //pegar resto da divisão entre:
            mult = (mult * 10) % 11;
            //ser for maior ou igual a 10 o número recebe '0'
            if (mult >= 10) {
                mult = 0;
            }
            //verficiar se primeiro dígito está correto
            if (mult == dig1) {
                //repetir o passo todo agora com os 10 primeiros números incluindo o primeiro digito verificador
                mult = 0;
                cont = 0;
                //multiplicar de 10 a 2
                for (let i = 11; i >= 2; i--) {
                    mult += (cpf.charAt(cont) * i);
                    cont++;
                }
                //pegar resto da divisão entre:
                mult = (mult * 10) % 11;
                //ser for maior ou igual a 10 o número recebe '0'
                if (mult >= 10) {
                    mult = 0;
                }
                //verficiar se segundo dígito está correto
                if (mult == dig2) {
                    return true;
                }
            } else {
                return false;
            }
        }
    }

    return false;
}

// Combo de Cliente
function CarregarComboCliente() {

    $.ajax({
        url: '/Beneficiario/CarregarComboCliente',
        type: 'GET',
        dataType: 'Json',
        async: true,
        success: function (response) {

            $('#IdCliente').append($('<option></option>').val('').text('Selecione'));

            $.each(response, function (i, row) {
                console.log(row);
                $('#IdCliente').append($('<option></option>').val(row.Id).text(row.Nome + ' ' + row.Sobrenome));
            });
        }
    });
}