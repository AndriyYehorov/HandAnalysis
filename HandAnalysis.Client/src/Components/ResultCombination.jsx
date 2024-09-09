export default function ResultCombination({combination}) {

    return (
      <>      
        <div className='grid grid-cols-2 gap-2'>
          <h3>Your Combination:</h3>
          <h3>{combination}</h3>
        </div>
      </>
    )
}