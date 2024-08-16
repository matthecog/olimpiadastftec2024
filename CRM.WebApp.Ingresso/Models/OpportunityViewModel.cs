﻿using System;
using System.ComponentModel.DataAnnotations;

namespace CRM.WebApp.Ingresso.Models;

public class OpportunityViewModel : EntityBase
{
    public Guid OpportunityID { get; set; }

    public Guid? CustomerID { get; set; }
    public Guid? LeadID { get; set; }

    [Required(ErrorMessage = "O campo Descrição é obrigatório.")]
    [StringLength(1000, ErrorMessage = "A Descrição não pode exceder 1000 caracteres.")]
    public string Description { get; set; }

    [Required(ErrorMessage = "O campo Nome é obrigatório.")]
    [StringLength(200, MinimumLength = 3, ErrorMessage = "O Nome deve ter entre 3 e 200 caracteres.")]
    public string Name { get; set; }

    [Required(ErrorMessage = "O campo Valor Estimado é obrigatório.")]
    [Range(0, double.MaxValue, ErrorMessage = "O Valor Estimado deve ser um valor positivo.")]
    public decimal EstimatedValue { get; set; }

    [Required(ErrorMessage = "O campo Data de Fechamento Esperada é obrigatório.")]
    public DateTime ExpectedCloseDate { get; set; }

    public Guid? CreatedBy { get; set; }
    public Guid? ModifiedBy { get; set; }
    public DateTime? CreatedOn { get; set; }
    public DateTime? ModifiedOn { get; set; }
    public int? StatusCode { get; set; }

    public string? LeadName { get; set; }
    public string? CustomerName { get; set; }
}