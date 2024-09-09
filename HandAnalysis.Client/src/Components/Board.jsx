import CardSlot from "./CardSlot"

export default function Board({cards}) {
    
    return (
        <>
            <h3>Board:</h3>

            <div className='h-40 flex justify-center gap-4'> 
                {cards.map((card, index) => (     
                    <CardSlot key = {index} rank={card.rank} suit={card.suit} />
                ))} 
            </div>

            <hr className="h-2 bg-orange-500 w-full m-10" />  
        </>
  )
}