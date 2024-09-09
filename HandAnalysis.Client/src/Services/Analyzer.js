import axios from "axios";

export const analyzeHand = async (cards) => {
    try{

        const updatedCards = cards.filter(card => card.rank != "").map(card => {            
            let newRank;
            switch (card.rank) {
              case 'A':
                newRank = 13;
                break;
              case 'K':
                newRank = 12;
                break;
              case 'Q':
                newRank = 11;
                break;
              case 'J':
                newRank = 10;
                break;
              default:
                newRank = card.rank - 1
                break;        
            }          
            
            let newSuit;
            switch (card.suit) {
              case '♦':
                newSuit = 1;
                break;
              case '♥':
                newSuit = 2;
                break;
              case '♠':
                newSuit = 4;
                break;
              case '♣':
                newSuit = 3;
                break;             
            }          
            
            return { rank: newRank, suit: newSuit };
        });

        var response = await axios.post("http://localhost:5072/hand", updatedCards);  

        return response.data;

    } catch(e){
        console.error(e);
    }    
}