import PlayCard from "./PlayCard"


export default function SuitCards({suit, onCardSelect}) {

  const ranks = ['A', 'K', 'Q', 'J', '10', '9', '8', '7', '6', '5', '4', '3', '2'];

  return (
    <>
      <div className='p-4 flex flex-row gap-2'>

      {ranks.map(rank => (
        <PlayCard 
          key={rank + suit}
          suit={suit}
          rank={rank} 
          onCardSelect={onCardSelect}
        />
      ))}

      </div>
    </>
  )
}