import SuitCards from "./SuitCards.jsx"

export default function Deck({onCardSelect}) {    

    return (
        <>
            <div className='grid grid-cols-2'>
                <SuitCards suit="♠" onCardSelect={onCardSelect}/>
                <SuitCards suit="♥" onCardSelect={onCardSelect}/>
                <SuitCards suit="♣" onCardSelect={onCardSelect}/>
                <SuitCards suit="♦" onCardSelect={onCardSelect}/>
            </div>              
                
            <hr className="h-2 bg-orange-500 w-full m-10" />  
        </>
    )
}