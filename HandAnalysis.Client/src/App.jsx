import './App.css'
import Deck from "./Components/Deck.jsx"
import Board from "./Components/Board.jsx"
import Hand from "./Components/Hand.jsx"
import ResultCombination from './Components/ResultCombination.jsx'
import { useState, useEffect } from 'react'
import { analyzeHand } from './Services/Analyzer.js'

export default function App() {

  const [boardCards, setBoardCards] = useState([
    { rank: "", suit: "" },
    { rank: "", suit: "" },
    { rank: "", suit: "" },
    { rank: "", suit: "" },
    { rank: "", suit: "" },
  ]);

  const [handCards, setHandCards] = useState([
    { rank: "", suit: "" },
    { rank: "", suit: "" },    
  ]);

  const [combination, SetCombination] = useState("");

  useEffect(() => {
    const AnalyzeHand = async() => {     
      if (
        handCards.findIndex((elem) => elem.rank == "") == -1 && 
        boardCards.filter((elem) => elem.rank == "").length <= 2
      ){
        let combination = await analyzeHand(handCards.concat(boardCards));  
        SetCombination(combination);
      }       
    };

    AnalyzeHand();
  }, [boardCards]);

  const handleCardSelect = (card) => {     

    const emptyIndexHand = handCards.findIndex((elem) => elem.rank == "");

    if(emptyIndexHand != -1){
      const updatedCards = [...handCards];
      updatedCards[emptyIndexHand] = {rank: card.rank, suit: card.suit};    
      setHandCards(updatedCards);
    }

    else {

      const emptyIndexBoard = boardCards.findIndex((elem) => elem.rank == "");

      if (emptyIndexBoard != -1 ){      
        const updatedCards = [...boardCards];
        updatedCards[emptyIndexBoard] = {rank: card.rank, suit: card.suit};    
        setBoardCards(updatedCards);
      }

      else {
        console.log("Arrays are full");
      }     
    }      
  };  

  return (
    <>      
      <section className="p-8 flex flex-col justify-center items-center">

        <Deck onCardSelect={handleCardSelect}/>
        
        <Board cards={boardCards}/>        

        <Hand cards={handCards}/>        
        
        <ResultCombination combination={combination} />

      </section>
    </>    
  )
}