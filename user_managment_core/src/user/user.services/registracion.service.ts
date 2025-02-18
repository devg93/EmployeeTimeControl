import { Injectable } from '@nestjs/common';
import * as bcrypt from 'bcrypt';
import { CreateRegistracionDto } from '../dto/create-registracion.dto';
import { UpdateRegistracionDto } from '../dto/update-registracion.dto';
import { InjectRepository } from '@nestjs/typeorm';
import { JwtService } from '@nestjs/jwt';
import { User } from '../entities/registracion.entity';
import { FindOperators, Repository } from 'typeorm';
import { userModule } from '../user.module';
import { EventEmitter2 } from '@nestjs/event-emitter';


@Injectable()
export class RegistracionService {
  constructor(
    @InjectRepository(User) private readonly userRepository: Repository<User>,
    private jwtService: JwtService,private eventEmitter: EventEmitter2
  ) { }
//*********************************************************** */
  async register(body: CreateRegistracionDto) {
    const { userName, email, password, iPadrres, deviceName } = body;

  

    const user = await this.userRepository.findOne({ where: { email } });
    if(user) return "user arledy exsit"
    const hashedPassword = await bcrypt.hash(password, 10);

    const newUser = this.userRepository.create({
      userName,
      email,
      passWord: hashedPassword,
      iPadrres: iPadrres ?? '0.0.0.0',
      deviceName: deviceName ?? 'Unknown Device',
    });
    this.eventEmitter.emit('user.created', {
      body
      
    });

    return this.userRepository.save(newUser);
  }
//*********************************************************** */
  async findAll(): Promise<User[]> {
    return await this.userRepository.find();
  }
//*********************************************************** */
  async findOneUser(id: number) {
    return await this.userRepository.findOne({ where: { id } });

  }
//*********************************************************** */
  async updateUser(id: number, updateRegistracionDto: UpdateRegistracionDto) {
    const user = await this.findOneUser(id);
    if (user == null) return "user not found"

    return await this.userRepository.update(id, updateRegistracionDto) ?? "user not updated"
  }
//*********************************************************** */
  async remove(id: number) {
    return await this.userRepository.delete(id);
  }
  
}
