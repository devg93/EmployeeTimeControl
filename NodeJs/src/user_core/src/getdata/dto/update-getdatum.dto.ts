import { PartialType } from '@nestjs/mapped-types';
import { CreateGetdatumDto } from './create-getdatum.dto';

export class UpdateGetdatumDto extends PartialType(CreateGetdatumDto) {}
