import { Injectable, NotFoundException, UnauthorizedException } from '@nestjs/common';
import { JwtService } from '@nestjs/jwt';
import { InjectRepository } from '@nestjs/typeorm';
import { Repository } from 'typeorm';
import * as bcrypt from 'bcryptjs';
import { User } from 'src/user/entities/registracion.entity';
import { CreateRegistracionDto } from 'src/user/dto/create-registracion.dto';
import { getProfileDto } from 'src/user/dto/getProfileDto';
import { promises } from 'dns';


@Injectable()
export class AuthService {
  constructor(
    @InjectRepository(User) private readonly userRepository: Repository<User>,
    private jwtService: JwtService,
  ) { }

  
  async validateUser(email: string, password: string): Promise<any> {
    
    const user = await this.userRepository.findOne({ where: { email } });
    
    if (user && (await bcrypt.compare(password, user.passWord))) {
      const { passWord, ...result } = user;
     
      return result;
    }
    return null;
  }
  //*************************************************************************/

  async login(user: any) {
    const payload = { username: user.email, sub: user.id };
    return {
      access_token: this.jwtService.sign(payload),
    };
}

//*************************************************************************

  async register(body: CreateRegistracionDto) {
    const { userName, email, password, iPadrres, deviceName } = body; 
  
    
    const hashedPassword = await bcrypt.hash(password, 10);
  
    const newUser = this.userRepository.create({
      userName,
      email,
      passWord: hashedPassword,
      iPadrres: iPadrres ?? '0.0.0.0', 
      deviceName: deviceName ?? 'Unknown Device',
    });
  
    return this.userRepository.save(newUser);
  }
  
  async getProfileById(body: getProfileDto): Promise<any> {

    const { email } = body;


    const user = await this.userRepository.findOne({ where: { email } });



    if (!user) {
        throw new NotFoundException('User not found');
    }

    return user;
}

  
//*************************************************************************


}



