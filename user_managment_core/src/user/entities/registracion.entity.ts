import { Entity, PrimaryGeneratedColumn, Column } from 'typeorm';

@Entity()
export class User {
  @PrimaryGeneratedColumn()
  id: number;

  @Column()
  userName: string;

  @Column()
  passWord: string;

  @Column()
  email: string

  @Column()
  iPadrres: string;

  @Column()
  deviceName: string;

  @Column({ default: false })
  staus: boolean=false;
}
