﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using kiyas.la.Context;
using kiyas.la.Entities;

namespace kiyas.la.User.Products
{
    public partial class Detay : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString["id"] != null)
                {
                    int id = int.Parse(Request.QueryString["id"].ToString());
                    LoadProductsById(id);
                    LoadComments(id);
                }
            }
        }

        private void LoadProductsById(int id)
        {
            using (KiyaslaContext db = new KiyaslaContext())
            {
                ListView1.DataSource = (from i in db.SmartPhone
                                        where i.Id == id
                                        select i).ToList();
                ListView1.DataBind();
            }
        }

        private void LoadProducts()
        {
            using (KiyaslaContext db = new KiyaslaContext())
            {
                ListView1.DataSource = db.SmartPhone.ToList();
                ListView1.DataBind();
            }
        }
        private void LoadComments(int id)
        {
            using (KiyaslaContext db = new KiyaslaContext())
            {
                CommentView.DataSource = (from i in db.Yorum
                                          where i.ProductId == id
                                          select i).ToList();
                CommentView.DataBind();
            }
        }

        protected void YrmEkle_Click(object sender, EventArgs e)
        {
            using (KiyaslaContext db = new KiyaslaContext())
            {
                int id = int.Parse(Request.QueryString["id"].ToString());
                Comment c = new Comment();
                c.Yorum = Yorum.Text;
                c.Name = Isim.Text;
                c.ProductId = id;
                db.Yorum.Add(c);
                db.SaveChanges();
                LoadComments(id);
            }
        }

    }
}