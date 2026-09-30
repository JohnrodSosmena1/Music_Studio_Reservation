using System;
using System.Drawing;
using System.Windows.Forms;
using CRM.winforms.DTOs;
using CRM.winforms.Helpers;

namespace CRM_MusicStudioReservation.Forms.Terms
{
    public partial class TermsViewForm : Form
    {
        public TermsViewForm(TermsDetailDto terms)
        {
            InitializeComponent();

            lblCode.Text = terms.TandCCode;
            lblType.Text = FormatType(terms.TandCType);
            lblVersion.Text = $"v{terms.Version}";
            lblStatus.Text = terms.Status;
            lblTitle.Text = terms.Title;
            txtContent.Text = terms.Content;
            lblAuthor.Text = $"Author: {terms.AuthorName ?? "Unknown"} ({terms.CreatedAt:yyyy-MM-dd HH:mm})";
            lblApprover.Text = terms.ApprovedByName != null 
                ? $"Approved by: {terms.ApprovedByName} ({terms.ApprovedAt:yyyy-MM-dd HH:mm})"
                : "Not yet approved";
            lblReAccept.Text = terms.RequiresReAcceptance ? "⚠ Mandatory Re-acceptance Required" : "Standard Policy";
            lblChangeNotes.Text = string.IsNullOrWhiteSpace(terms.ChangeNotes) ? "None" : terms.ChangeNotes;

            lblStatus.ForeColor = terms.Status switch
            {
                "Published" => AppTheme.Success,
                "PendingApproval" => AppTheme.Info,
                "Draft" => AppTheme.Warning,
                _ => AppTheme.TextSecondary
            };
        }

        private static string FormatType(string type) => type switch
        {
            "GeneralTerms" => "General Terms",
            "BookingPolicy" => "Booking Policy",
            "CancellationPolicy" => "Cancellation & Refund Policy",
            "PrivacyPolicy" => "Privacy Policy",
            "StudioUsageRules" => "Studio Usage Rules",
            _ => type
        };

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
