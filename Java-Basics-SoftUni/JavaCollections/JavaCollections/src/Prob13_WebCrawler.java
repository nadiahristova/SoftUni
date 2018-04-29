import java.io.IOException;
import java.io.PrintWriter;
import java.util.ArrayList;
import java.util.HashSet;
import java.util.Scanner;
import java.util.regex.Matcher;
import java.util.regex.Pattern;

import org.jsoup.Jsoup;
import org.jsoup.nodes.Document;
import org.jsoup.nodes.Element;
import org.jsoup.select.Elements;

//The program uses the Jsoup library

public class Prob13_WebCrawler {
	public static void main (String[] args) throws IOException {
		Scanner input = new Scanner(System.in);
		System.out.print("Enter an url for seed or just press Enter for default[http://www.vimalkumarpatel.blogspot.com]: ");
		String seed = input.nextLine();
		if (!seed.equals("")) {
			String webRegex = "(https?:\\/\\/)([\\da-z\\.-]+)\\.([a-z\\.]{2,6})([\\/\\w \\.-]*)*\\/?$"; 
			Pattern pattern = Pattern.compile(webRegex);
			Matcher match = pattern.matcher(seed);
			if (!match.find()) {
				System.out.println("Not a valid entry! Please try again.");
				return;
			}
		} else seed = "http://www.vimalkumarpatel.blogspot.com/";		
		
		System.out.print("Enter the number of levels: ");		
		int recursions = Integer.parseInt(input.nextLine());
		ArrayList<String> listOfUrls = new ArrayList<>();		
		listOfUrls.add(seed);
		ArrayList<String> listOfUrlsInRecursio = new ArrayList<>();
		listOfUrlsInRecursio.add(seed);
		ArrayList<String> answers = new ArrayList<>();
		answers.add("\t\tThe seed: " + seed + "\n");		
		
		for (int level = 1; level <= recursions; level++) {				
			ArrayList<String> listOfUrlsPerCurrURL = retrieveUrls(listOfUrlsInRecursio);
			listOfUrlsInRecursio.clear();
			for(String subURL : listOfUrlsPerCurrURL){
				listOfUrlsInRecursio.add(subURL);
			}
			
			ArrayList<String> mediatorList = new ArrayList<>();			
			mediatorList.addAll(listOfUrlsInRecursio);		
			
			mediatorList.retainAll(listOfUrls);
			listOfUrlsInRecursio.removeAll(mediatorList);
			listOfUrls.addAll(listOfUrlsInRecursio);
			
			for(String URL: listOfUrlsInRecursio){
				answers.add(String.format("%d level - %s", level, URL));
			}
		}
		
		try (PrintWriter writer = new PrintWriter("WebCrawerOutput.txt", "UTF-8")) {			
			for (String price : answers) {
				writer.println(price);
			}				
		} catch (IOException e) {
			e.printStackTrace();
		}
	}
	
    public static ArrayList<String> retrieveUrls(ArrayList<String> listOfUrls) throws IOException {        
    	HashSet<String>  listOfHTMLHrefs = new HashSet<>();
    	
        for (String crawUrl : listOfUrls) {		
	        String regex = ".\\.html$";
	        Pattern pattern = Pattern.compile(regex);
	        
	        print("Fetching %s...", crawUrl);
	
	        Document doc = Jsoup.connect(crawUrl).ignoreHttpErrors(true).get();
	        Elements links = doc.select("a[href]");
	        Elements imports = doc.select("link[href]");
	        print("\nLinks: (%d) found.", links.size());
	        for (Element link : links) {	        	        	
	        	String page =  link.attr("abs:href");
	        	Matcher match = pattern.matcher(page);
	        	if (match.find()) {
	        		 print("%s  (%s)", link.attr("abs:href"), trim(link.text(), 35));
	        		 listOfHTMLHrefs.add(page);
				}	        	
	        }
	        
	        print("\nImports: (%d) found.", imports.size());
	        for (Element link : imports) {	        	        	
	        	String page =  link.attr("abs:href");
	        	Matcher match = pattern.matcher(page);
	        	if (match.find()) {
	        		 print("%s  (%s)", link.attr("abs:href"), link.attr("rel"));
	        		 listOfHTMLHrefs.add(page);
				}	        	
	        }
	        

        }
        
        return new ArrayList<String>(listOfHTMLHrefs);
    }

    private static void print(String msg, Object... args) {
        System.out.println(String.format(msg, args));
    }

    private static String trim(String s, int width) {
        if (s.length() > width)
            return s.substring(0, width-1) + ".";
        else
            return s;
    }
}
