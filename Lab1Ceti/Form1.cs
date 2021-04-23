using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


// Create a C# application that accepts a lunch order from the user and calculates the order subtotal and total with 5% tax added
// April 21, 2012; Ceti Zyko

namespace Lab1Ceti
{
    public partial class Form1 : Form

    // Declaration as constants of All fixed values in the application to elemnate "hardcoding' values
    {
        const decimal BurgerPrice = 6.95m; // fixed value for Hamburger price
        const decimal PizzaPrice = 5.95m;  // fixed value for Pizza price
        const decimal SaladPrice = 4.95m;  // fixed value for Salad price

        const decimal BurgeraddOn = 0.75m;  // fixed value for Hamburger AddOn price, Letucce ...
        const decimal PizzaaddOn = 0.50m;   // fixed value for Pizza AddOn price, Pepperoni ...
        const decimal SaladaddOn = 0.25m;   // fixed value for Salad AddOn price, Croutons ...
        const decimal TaxRate = 0.05m;      // fixed TaxRate for GST calculations

        // Declaration of parameters/variables to be used for calculation of Subtotal, tax, and Total order
        decimal addOn = 0m;                 // parameter to be used for calculation of AddedPrice
        decimal basePrice = 0m;             // parameter to be used for calculation of AddedPrice
        decimal mySubtotal = 0m;            // parameter to be used for calculation of Subtotal price
        decimal tax = 0m;                   // parameter to be used for calculation of tax
        decimal myTotal = 0m;               // parameter to be used for calculation of Total Oreder price

        int addOnCounter;

        // Declaration in strings of the "Add-on items" that belong to groupBox2 also called "Add-on items" box
        // the "Add-ond items" declared in "strings" can be added onto the Main Course meal
        string[] groupBox2name = { "Add-on items($.75 / each)", "Add-on items ($.50/each)", "Add-on items ($.25/each)" };
        string[] BurgeraddOns = { "Lettuce, Tomato, Onions", "Ketchup,Mustard, Mayo", "French Fries" };
        string[] PizzaaddOns = { "Pepperoni", "Sausage", "Olives" };
        string[] SaladaddOns = { "Croutons", "Bacon bits", "Bread sticks" };

        public Form1()
        {
            InitializeComponent();
        }

        // One Radio button is selected at a time
        // The default selected Radio button is Hamburger
        private void radHamburger_CheckedChanged(object sender, EventArgs e)
        {
          
            RadioButton s = (RadioButton)sender;
            switch (s.Name)
            {
                case "radPizza":
                    DisplayAddOns(groupBox2name[1], PizzaaddOns, PizzaaddOn, PizzaPrice);
                    break;
                case "radSalad":
                    DisplayAddOns(groupBox2name [2], SaladaddOns, SaladaddOn, SaladPrice);
                    break;
                default:
                    DisplayAddOns(groupBox2name[0], BurgeraddOns, BurgeraddOn, BurgerPrice);
                    break;
            }
        }

        // After one Radio button is selected then "Add-ons items" of groupBox2 can be added
        // checkbox1 for Lettuce, checkbox2 for Ketchup, checkbox3 for French fries is the array [list] with prices of items
        // that can be addedOn to the Main Course basePrice selected
        private void DisplayAddOns(string boxName, string[] addOnList,
            decimal addOnPrice, decimal bPrice)
        {
            groupBox2.Text = boxName;
            checkBox1.Text = addOnList[0];
            checkBox2.Text = addOnList[1];
            checkBox3.Text = addOnList[2];
            addOn = addOnPrice;
            basePrice = bPrice;

        }

        // Place Order button
        // User will click on Place Order button to place his/her order after has made the selections from Main Course and/or "Add-ons items" groups
        // The Alt-key is defined as: Alt + r, and the form has "Place Order" as Accept button.
        private void bttnPlOrder_Click(object sender, EventArgs e)
        {
            addOnCounter = 0;
            AddOnsCheck();
            Subtotalcalc();
            OrderCalc(mySubtotal, out tax, out myTotal);
            DisplayForm();

        }
         // Subtotal, Tax, and Order Total are displayed as a group with DisplayForm function
        private void DisplayForm()
        {
            lblSubtotal.Text = mySubtotal.ToString("c");
            lblTax.Text = tax.ToString("c");
            lblTotal.Text = myTotal.ToString("c");
        }
        //Calculation of tax amount and order total is done in a separate programmer-defined method
        private void OrderCalc(decimal subtotal, out decimal calcTax, out decimal calcTotal)
        {
            calcTax = TaxRate * subtotal;
            calcTotal = calcTax + subtotal;
        }

        // The programmer-defined method accepts subtotal as a parameter passed by value.
        // it uses out parameter(s) and/or return statemnt
        private void Subtotalcalc()
        {
            mySubtotal = basePrice + addOn * addOnCounter;
        }

        // When/If checkBox1, 2, and 3 are CheckedIn then "Add-ons items" are addeed/counted.
        // AddOnsCheck() function takes care of calaculations for the Main Course selected 
        private void AddOnsCheck()
        {
            if (checkBox1.Checked)
                addOnCounter++;
            if (checkBox2.Checked)
                addOnCounter++;
            if (checkBox3.Checked)
                addOnCounter++;
        }

        // Reset button
        // User can click on Reset button to cancel his/her order.
        // The Alt-key is defined as: Alt + e, and the form has Reset as Cancel button.
        private void bttnReset_Click(object sender, EventArgs e)
        {
            mySubtotal = 0m;
            tax = 0m;
            myTotal = 0m;
            addOn = BurgeraddOn;
            basePrice = BurgerPrice;
            addOnCounter = 0;
            lblSubtotal.Text = "";
            lblTax.Text = "";
            lblTotal.Text = "";
            checkBox1.Checked = false;
            checkBox2.Checked = false;
            checkBox3.Checked = false;
            radHamburger.Checked = false;
            radPizza.Checked = false;
            radSalad.Checked = false;
        }

        // Exit button
        // User can click on the Exit button or the Alt-key defined as: Alt + x to exit the order.
        // Exit button terminates application
        private void bttnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
