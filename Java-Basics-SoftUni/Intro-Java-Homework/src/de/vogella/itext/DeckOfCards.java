//This program is created with the help of the IText JAVA PDF Library
//The cards in the PDF are ordered either by default or randomly, a choice that can be made in the beginning of the execution of the program
package de.vogella.itext;

import java.io.FileOutputStream;
import java.util.ArrayList;
import java.util.Random;
import java.util.Scanner;

import com.itextpdf.awt.geom.Rectangle;
import com.itextpdf.text.BaseColor;
import com.itextpdf.text.Document;
import com.itextpdf.text.DocumentException;
import com.itextpdf.text.Element;
import com.itextpdf.text.Font;
import com.itextpdf.text.Font.FontFamily;
import com.itextpdf.text.FontFactory;
import com.itextpdf.text.Image;
import com.itextpdf.text.PageSize;
import com.itextpdf.text.Paragraph;
import com.itextpdf.text.Phrase;
import com.itextpdf.text.pdf.BaseFont;
import com.itextpdf.text.pdf.PdfContentByte;
import com.itextpdf.text.pdf.PdfGState;
import com.itextpdf.text.pdf.PdfWriter;


public class DeckOfCards {
  private static String FileExpLoc = "/home/nadia/Java/DeckOfCards.pdf"; 
  //Sets the path and the name(DeckOfCards.pdf) of the newly created PDF
  private static String FONT = "/home/nadia/java/Intro-Java-Homework/lib/CARDC___.TTF"; 
//Keeps the path to the wanted font, in our case - CARDC___.TTF, The font is located in the "lib" folder

  public static void main(String[] args) {
    try {
    	Document document = new Document(PageSize.A3.rotate());
        PdfWriter.getInstance(document, new FileOutputStream(FileExpLoc));
        PdfWriter writer
        = PdfWriter.getInstance(document, new FileOutputStream(FileExpLoc));
        document.open();  
      //int[][] positionHolderSigns = new int[][]{{7,70},{7,83},{58,8},{58,21},{18,34}};
        
        Scanner in = new Scanner(System.in);
        System.out.print("Would you like a random ordination of the cards in the deck or a default one?[default]: ");
        String answer = in.nextLine();
        in.close();
        
        int leftX = 20;
        int bottoY = 550;
        String[][] cards = cardGen(answer);
        
        Random gen = new Random();
        int imi = gen.nextInt(6);
        String ImgHomeFolder = "/home/nadia/java/Intro-Java-Homework/lib/Img/" + Integer.toString(imi) + ".jpg";
        //Gives the path and the name of the jpg-files needed for the background of the PDF. The pictures are located in the "lib" folder
        Image img = Image.getInstance(ImgHomeFolder);
        img.setAbsolutePosition(
                (PageSize.A3.getHeight() - img.getScaledWidth()) / 2,
                (PageSize.A3.getWidth() - img.getScaledHeight()) / 2);      
        document.add(img);
        
        addTitlePage(document, answer); 
        addMetaData(document);
        
        PdfContentByte canv = writer.getDirectContent();
    	  
        canv.saveState();
        PdfGState gs1 = new PdfGState(); 
        gs1.setFillOpacity(0.4f);
        canv.setGState(gs1);
        Rectangle rect = new Rectangle(10, 192, 1177, 470);
        canv.roundRectangle(
                (float)rect.x + 3f, (float)rect.y + 3f, (float)rect.getWidth() - 6,
                (float)rect.getHeight() - 6, 4);
        canv.setLineWidth(3f);
        canv.setColorStroke(BaseColor.LIGHT_GRAY);            
        canv.fillStroke();
        canv.restoreState();
        
        for (int i = 1; i <= cards.length; i++) {
        	
        	PdfContentByte canvas = writer.getDirectContent();
      	  
            canvas.saveState();
            canvas.setColorFill(BaseColor.WHITE);
            rect = new Rectangle(leftX, bottoY, 75, 100);
            canvas.roundRectangle(
                    (float)rect.x + 1.5f, (float)rect.y + 1.5f, (float)rect.getWidth() - 3,
                    (float)rect.getHeight() - 3, 4);
            canvas.setLineWidth(1.5f);
            canvas.setColorStroke( BaseColor.LIGHT_GRAY);            
            canvas.fillStroke();
            canvas.restoreState();
      		
            PdfContentByte signs = writer.getDirectContent();
            
            signs.saveState(); 
            signs.beginText();  
            int ini = i-1;
            BaseFont bf = BaseFont.createFont(FONT,BaseFont.IDENTITY_H,true);      
            signs.setFontAndSize(bf,14);
            
            if (cards[ini][1].equals("\u2666") || cards[ini][1].equals("\u2665")) {
          	  signs.setColorFill(BaseColor.RED);
            } else {
      		signs.setColorFill(BaseColor.BLACK);
            }
            
            //This section here is not working, sadly
//            for (int i = 1; i <= positionHolderSigns.length; i++) {
//          	String gr = "\u2666";
//          	int num = i%2;
//      		if (i == 5) {
//      			signs.setFontAndSize(bf,45);
//      			signs.setTextMatrix(20+positionHolderSigns[i-1][0], 550+positionHolderSigns[i-1][1]);
//      			signs.showText(gr);
//      		} else if (num != 0) {
//      			//signs.setFontAndSize(bf,12);
//      			signs.setTextMatrix(20+positionHolderSigns[i-1][0], 550+positionHolderSigns[i-1][1]);
//      			signs.showText(gr);
//      		} else {
//      			signs.setTextMatrix(20+positionHolderSigns[i-1][0], 550+positionHolderSigns[i-1][1]);
//      			signs.showText("J");
//      		}
//      	  }
            signs.setTextMatrix(leftX+56, bottoY+8);
            signs.showText(cards[ini][0]);
            signs.setTextMatrix(leftX+8, bottoY+83);
            signs.showText(cards[ini][0]);
            signs.setTextMatrix(leftX+56, bottoY+22);     
            String suit = cards[ini][1];
            signs.showText(suit);
            signs.setTextMatrix(leftX+8, bottoY+69);
            signs.showText(suit);  
            signs.setFontAndSize(bf,45);
            signs.setTextMatrix(leftX+18, bottoY+34);
            signs.showText(suit);     
            signs.endText();
            signs.restoreState();
            
            if (i % 4 == 0 ) {
				 leftX +=90;
				 bottoY +=345;
			}else {		 
				bottoY -=115;
			}
		}  
        
        document.close();
        
    } catch (Exception e) {
      e.printStackTrace();
    }
  }

  private static String[][] cardGen(String kind) {
	String[][] deckOfCards= new String[52][2];
	String suits = "\u2665\u2666\u2663\u2660";
	
	Random generator = new Random();
	
	if (kind.equals("random")) {
		
		ArrayList<String> deck = new ArrayList<String>();
		for (int i = 0; i < 13; i++) {
			deck.add(suits);
		}
		
		ArrayList<Integer> posHolder = new ArrayList<Integer>();
		for (int i = 0; i < 13; i++) {
			posHolder.add(i);
		}
		
		for (int i = 0; i < 52; i++) {
			int facePos = generator.nextInt(posHolder.size());
			int face = posHolder.get(facePos);
			
			int whichSuit = generator.nextInt((deck.get(face)).length());
			
			switch (face+1) {
			case 1: deckOfCards[i][0] = "A"; break;
			case 11: deckOfCards[i][0] = "J"; break;
			case 12: deckOfCards[i][0] = "Q"; break;
			case 13: deckOfCards[i][0] = "K"; break;
			default: deckOfCards[i][0] = Integer.toString(face+1); break;
			}
			deckOfCards[i][1] = "" + (deck.get(face)).charAt(whichSuit);
			deck.set(face,removeCharAt(deck.get(face),whichSuit));		
			if (deck.get(posHolder.get(facePos)).length() == 0) {
				posHolder.remove(facePos);	
			} 			
		} 
	} else {
		for (int i = 0; i < 52; i++) {
			int num = i/4+1;
			int num1 = i%4;			
			switch (num) {
			case 1: deckOfCards[i][0] = "A"; break;
			case 11: deckOfCards[i][0] = "J"; break;
			case 12: deckOfCards[i][0] = "Q"; break;
			case 13: deckOfCards[i][0] = "K"; break;
			default: deckOfCards[i][0] = Integer.toString(num); break;
			}
			deckOfCards[i][1] = "" + suits.charAt(num1);
		}
	}		
	
	return deckOfCards;
}

  private static void addMetaData(Document document) {
    document.addTitle("A Deck Of Cards");
    document.addSubject("My first PDF with the help of Java and iText");
    document.addKeywords("Java, PDF, iText");   
    document.addCreator("Nadia Hristova");
  }
  
  private static void addTitlePage(Document document, String kind)
	      throws DocumentException {	  
	    Font font = new Font(FontFamily.HELVETICA,35,Font.BOLDITALIC,BaseColor.WHITE);
	    String str = null;
	    if (kind.equals("random")) {
	    	str = "A Randomly Shuffled Deck Of Cards:";			
		} else {
			str = "A Deck Of Cards Ordered By Default:";
		}
	    
	    Paragraph title = new Paragraph(str, font);	     
	    title.setAlignment(Element.ALIGN_CENTER);
	    for (int i = 0; i < 4; i++) {
	    	document.add(new Phrase("\n"));
		}	  
	    
	    document.add(title);    
  }
  
  private static String removeCharAt(String string, int whichSuit) {
		 return string.substring(0, whichSuit) + string.substring(whichSuit + 1);		
  }  
} 
